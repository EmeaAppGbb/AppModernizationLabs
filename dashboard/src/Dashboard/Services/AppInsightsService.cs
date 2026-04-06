using System.Net.Http.Headers;
using System.Text.Json;
using Dashboard.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Dashboard.Services;

/// <summary>
/// Queries Application Insights REST API to fetch telemetry data for the gallery site.
/// Uses Kusto (KQL) queries to pull custom events, page views, and session data.
/// </summary>
public sealed class AppInsightsService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMemoryCache _cache;
    private readonly IConfiguration _config;
    private readonly ILogger<AppInsightsService> _logger;
    private readonly TimeSpan _cacheTtl = TimeSpan.FromMinutes(5);

    private string AppId => _config["AppInsights:AppId"] ?? string.Empty;
    private string ApiKey => _config["AppInsights:ApiKey"] ?? string.Empty;
    private bool IsConfigured => !string.IsNullOrEmpty(AppId) && !string.IsNullOrEmpty(ApiKey);

    public AppInsightsService(
        IHttpClientFactory httpClientFactory,
        IMemoryCache cache,
        IConfiguration config,
        ILogger<AppInsightsService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _cache = cache;
        _config = config;
        _logger = logger;
    }

    // ── Page Views ──────────────────────────────────────────────

    public async Task<PageViewSummary> GetPageViewSummaryAsync()
    {
        return await CachedQuery("pageview-summary", async () =>
        {
            var today = await RunKustoQueryAsync<PageViewMetric>(KustoQueries.PageViewsToday);
            var week = await RunKustoQueryAsync<PageViewMetric>(KustoQueries.PageViews7Days);
            var month = await RunKustoQueryAsync<PageViewMetric>(KustoQueries.PageViews30Days);
            var uniqueUsers = await RunKustoQueryAsync<PageViewMetric>(KustoQueries.UniqueUsers30Days);
            var topPages = await RunKustoQueryAsync<PageViewMetric>(KustoQueries.TopPages);
            var overTime = await RunKustoQueryAsync<PageViewMetric>(KustoQueries.PageViewsOverTime);
            var geo = await RunKustoQueryAsync<GeoMetric>(KustoQueries.GeographicDistribution);
            var devices = await RunKustoQueryAsync<DeviceMetric>(KustoQueries.DeviceBreakdown);

            return new PageViewSummary
            {
                TotalViewsToday = today.FirstOrDefault()?.ViewCount ?? 0,
                TotalViews7Days = week.FirstOrDefault()?.ViewCount ?? 0,
                TotalViews30Days = month.FirstOrDefault()?.ViewCount ?? 0,
                TotalUniqueUsers = uniqueUsers.FirstOrDefault()?.UniqueUsers ?? 0,
                ActiveUsersNow = 0,
                TopPages = topPages,
                ViewsOverTime = overTime,
                GeographicDistribution = geo,
                DeviceBreakdown = devices
            };
        });
    }

    // ── Lab Engagement ──────────────────────────────────────────

    public async Task<LabEngagementSummary> GetLabEngagementAsync()
    {
        return await CachedQuery("lab-engagement", async () =>
        {
            var topLabs = await RunKustoQueryAsync<LabEngagementMetric>(KustoQueries.TopLabClicks);
            var actionBreakdown = await RunKustoQueryAsync<LabEngagementMetric>(KustoQueries.LabActionBreakdown);
            var filters = await RunKustoQueryAsync<FilterUsageMetric>(KustoQueries.FilterUsage);

            var summary = actionBreakdown.FirstOrDefault();
            return new LabEngagementSummary
            {
                TopLabs = topLabs,
                TotalCloneClicks = summary?.CloneClicks ?? 0,
                TotalVsCodeClicks = summary?.VsCodeClicks ?? 0,
                TotalCodespaceClicks = summary?.CodespaceClicks ?? 0,
                TotalStartMenuInteractions = summary?.StartMenuInteractions ?? 0,
                TotalStarClicks = summary?.StarClicks ?? 0,
                FilterUsagePatterns = filters
            };
        });
    }

    // ── User Activity ───────────────────────────────────────────

    public async Task<UserActivitySummary> GetUserActivityAsync()
    {
        return await CachedQuery("user-activity", async () =>
        {
            var sessions = await RunKustoQueryAsync<UserActivityMetric>(KustoQueries.SessionsOverTime);
            var themes = await RunKustoQueryAsync<DeviceMetric>(KustoQueries.ThemePreference);
            var searches = await RunKustoQueryAsync<SearchQueryMetric>(KustoQueries.TopSearchQueries);

            long darkUsers = themes.FirstOrDefault(t => t.Name == "dark")?.Count ?? 0;
            long lightUsers = themes.FirstOrDefault(t => t.Name == "light")?.Count ?? 0;

            return new UserActivitySummary
            {
                SessionsOverTime = sessions,
                OverallAvgSessionDuration = sessions.Any()
                    ? sessions.Average(s => s.AvgSessionDurationSeconds) : 0,
                TotalNewUsers = sessions.Sum(s => s.NewUsers),
                TotalReturningUsers = sessions.Sum(s => s.ReturningUsers),
                DarkThemeUsers = darkUsers,
                LightThemeUsers = lightUsers,
                TopSearchQueries = searches
            };
        });
    }

    // ── Video Metrics ───────────────────────────────────────────

    public async Task<VideoMetricSummary> GetVideoMetricsAsync()
    {
        return await CachedQuery("video-metrics", async () =>
        {
            var videos = await RunKustoQueryAsync<VideoMetric>(KustoQueries.VideoPlayEvents);
            var mostWatched = await RunKustoQueryAsync<VideoMetric>(KustoQueries.MostWatchedVideos);

            return new VideoMetricSummary
            {
                VideosByLab = videos,
                TotalPlays = videos.Sum(v => v.PlayCount),
                TotalCompletes = videos.Sum(v => v.CompleteCount),
                OverallAvgWatchTime = videos.Any()
                    ? videos.Average(v => v.AvgWatchTimeSeconds) : 0,
                MostWatched = mostWatched
            };
        });
    }

    // ── Share Metrics ───────────────────────────────────────────

    public async Task<ShareMetricSummary> GetShareMetricsAsync()
    {
        return await CachedQuery("share-metrics", async () =>
        {
            var shares = await RunKustoQueryAsync<ShareMetric>(KustoQueries.SharesByLab);
            var conversions = await RunKustoQueryAsync<ShareConversion>(KustoQueries.ShareConversions);

            return new ShareMetricSummary
            {
                SharesByLab = shares,
                TotalShares = shares.Sum(s => s.ShareCount),
                TotalWebShareApi = shares.Sum(s => s.WebShareApiCount),
                TotalClipboardCopy = shares.Sum(s => s.ClipboardCopyCount),
                ShareConversions = conversions
            };
        });
    }

    // ── Kusto Query Execution ───────────────────────────────────

    private async Task<List<T>> RunKustoQueryAsync<T>(string query) where T : new()
    {
        if (!IsConfigured)
        {
            _logger.LogWarning("Application Insights is not configured. Returning empty results.");
            return [];
        }

        try
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("x-api-key", ApiKey);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var url = $"https://api.applicationinsights.io/v1/apps/{AppId}/query";
            var payload = new { query };
            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return ParseKustoResponse<T>(json);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to execute Kusto query: {Query}", query[..Math.Min(query.Length, 100)]);
            return [];
        }
    }

    private static List<T> ParseKustoResponse<T>(string json) where T : new()
    {
        var results = new List<T>();

        using var doc = JsonDocument.Parse(json);
        var tables = doc.RootElement.GetProperty("tables");
        if (tables.GetArrayLength() == 0) return results;

        var table = tables[0];
        var columns = table.GetProperty("columns");
        var rows = table.GetProperty("rows");

        var columnNames = new List<string>();
        foreach (var col in columns.EnumerateArray())
        {
            columnNames.Add(col.GetProperty("name").GetString() ?? string.Empty);
        }

        var properties = typeof(T).GetProperties()
            .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

        foreach (var row in rows.EnumerateArray())
        {
            var item = new T();
            for (int i = 0; i < columnNames.Count && i < row.GetArrayLength(); i++)
            {
                if (!properties.TryGetValue(columnNames[i], out var prop)) continue;
                if (!prop.CanWrite) continue;

                var cell = row[i];
                try
                {
                    object? value = prop.PropertyType switch
                    {
                        Type t when t == typeof(string) => cell.GetString(),
                        Type t when t == typeof(long) => cell.GetInt64(),
                        Type t when t == typeof(int) => cell.GetInt32(),
                        Type t when t == typeof(double) => cell.GetDouble(),
                        Type t when t == typeof(DateTime) => cell.GetDateTime(),
                        Type t when t == typeof(bool) => cell.GetBoolean(),
                        _ => null
                    };
                    if (value != null) prop.SetValue(item, value);
                }
                catch { /* Skip cells that fail to parse */ }
            }
            results.Add(item);
        }

        return results;
    }

    // ── Caching ─────────────────────────────────────────────────

    private async Task<T> CachedQuery<T>(string key, Func<Task<T>> factory)
    {
        if (_cache.TryGetValue(key, out T? cached) && cached is not null)
            return cached;

        var result = await factory();
        _cache.Set(key, result, _cacheTtl);
        return result;
    }

    // ── Kusto Queries ───────────────────────────────────────────

    private static class KustoQueries
    {
        public const string PageViewsToday = @"
            pageViews
            | where timestamp >= ago(1d)
            | summarize ViewCount = count() by bin(timestamp, 1d)";

        public const string PageViews7Days = @"
            pageViews
            | where timestamp >= ago(7d)
            | summarize ViewCount = count() by bin(timestamp, 7d)";

        public const string PageViews30Days = @"
            pageViews
            | where timestamp >= ago(30d)
            | summarize ViewCount = count() by bin(timestamp, 30d)";

        public const string UniqueUsers30Days = @"
            pageViews
            | where timestamp >= ago(30d)
            | summarize UniqueUsers = dcount(user_Id)";

        public const string TopPages = @"
            pageViews
            | where timestamp >= ago(30d)
            | summarize ViewCount = count() by PageName = name, PageUrl = url
            | top 10 by ViewCount desc";

        public const string PageViewsOverTime = @"
            pageViews
            | where timestamp >= ago(30d)
            | summarize ViewCount = count() by bin(timestamp, 1d)
            | order by timestamp asc";

        public const string GeographicDistribution = @"
            pageViews
            | where timestamp >= ago(30d)
            | summarize Count = count() by Country = client_CountryOrRegion
            | top 15 by Count desc";

        public const string DeviceBreakdown = @"
            pageViews
            | where timestamp >= ago(30d)
            | summarize Count = count() by Name = client_Browser
            | top 10 by Count desc";

        public const string TopLabClicks = @"
            customEvents
            | where timestamp >= ago(30d) and name == 'LabClick'
            | extend LabName = tostring(customDimensions.labName)
            | summarize ClickCount = count(),
                CloneClicks = countif(tostring(customDimensions.action) == 'clone'),
                VsCodeClicks = countif(tostring(customDimensions.action) == 'vscode'),
                CodespaceClicks = countif(tostring(customDimensions.action) == 'codespace'),
                StarClicks = countif(tostring(customDimensions.action) == 'star')
              by LabName
            | top 20 by ClickCount desc";

        public const string LabActionBreakdown = @"
            customEvents
            | where timestamp >= ago(30d) and name in ('LabClick', 'StartMenuInteraction')
            | summarize
                CloneClicks = countif(tostring(customDimensions.action) == 'clone'),
                VsCodeClicks = countif(tostring(customDimensions.action) == 'vscode'),
                CodespaceClicks = countif(tostring(customDimensions.action) == 'codespace'),
                StarClicks = countif(tostring(customDimensions.action) == 'star'),
                StartMenuInteractions = countif(name == 'StartMenuInteraction')";

        public const string FilterUsage = @"
            customEvents
            | where timestamp >= ago(30d) and name == 'FilterApplied'
            | extend FilterName = tostring(customDimensions.filterName),
                     FilterValue = tostring(customDimensions.filterValue)
            | summarize UsageCount = count() by FilterName, FilterValue
            | top 20 by UsageCount desc";

        public const string SessionsOverTime = @"
            union (pageViews), (customEvents)
            | where timestamp >= ago(30d)
            | summarize
                SessionCount = dcount(session_Id),
                AvgSessionDurationSeconds = avg(session_Id),
                NewUsers = dcountif(user_Id, itemType == 'pageView'),
                ReturningUsers = dcountif(user_Id, itemType == 'customEvent')
              by bin(timestamp, 1d)
            | order by timestamp asc";

        public const string ThemePreference = @"
            customEvents
            | where timestamp >= ago(30d) and name == 'ThemeToggle'
            | extend Theme = tostring(customDimensions.theme)
            | summarize Count = count() by Name = Theme";

        public const string TopSearchQueries = @"
            customEvents
            | where timestamp >= ago(30d) and name == 'Search'
            | extend SearchQuery = tostring(customDimensions.query)
            | where isnotempty(SearchQuery)
            | summarize Count = count() by Query = SearchQuery
            | top 20 by Count desc";

        public const string VideoPlayEvents = @"
            customEvents
            | where timestamp >= ago(30d) and name in ('VideoPlay', 'VideoPause', 'VideoComplete')
            | extend LabName = tostring(customDimensions.labName),
                     VideoId = tostring(customDimensions.videoId),
                     VideoTitle = tostring(customDimensions.videoTitle)
            | summarize
                PlayCount = countif(name == 'VideoPlay'),
                PauseCount = countif(name == 'VideoPause'),
                CompleteCount = countif(name == 'VideoComplete'),
                AvgWatchTimeSeconds = avg(todouble(customDimensions.watchTimeSeconds))
              by LabName, VideoId, VideoTitle
            | order by PlayCount desc";

        public const string MostWatchedVideos = @"
            customEvents
            | where timestamp >= ago(30d) and name == 'VideoPlay'
            | extend LabName = tostring(customDimensions.labName),
                     VideoId = tostring(customDimensions.videoId),
                     VideoTitle = tostring(customDimensions.videoTitle)
            | summarize PlayCount = count() by LabName, VideoId, VideoTitle
            | top 10 by PlayCount desc";

        public const string SharesByLab = @"
            customEvents
            | where timestamp >= ago(30d) and name == 'ShareLab'
            | extend LabName = tostring(customDimensions.labName),
                     Method = tostring(customDimensions.shareMethod)
            | summarize
                ShareCount = count(),
                WebShareApiCount = countif(Method == 'webshare'),
                ClipboardCopyCount = countif(Method == 'clipboard')
              by LabName
            | order by ShareCount desc";

        public const string ShareConversions = @"
            customEvents
            | where timestamp >= ago(30d) and name in ('ShareLab', 'SharedLinkVisit')
            | extend LabName = tostring(customDimensions.labName)
            | summarize
                Shares = countif(name == 'ShareLab'),
                ResultingVisits = countif(name == 'SharedLinkVisit')
              by LabName
            | where Shares > 0
            | order by Shares desc";
    }
}
