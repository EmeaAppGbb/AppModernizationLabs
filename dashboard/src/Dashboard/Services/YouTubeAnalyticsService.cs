using System.Text.Json;
using Dashboard.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Dashboard.Services;

/// <summary>
/// Optional integration with YouTube Data API v3 for fetching video statistics.
/// Provides graceful fallback if not configured.
/// </summary>
public sealed class YouTubeAnalyticsService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMemoryCache _cache;
    private readonly IConfiguration _config;
    private readonly ILogger<YouTubeAnalyticsService> _logger;
    private readonly TimeSpan _cacheTtl = TimeSpan.FromMinutes(15);

    private string ApiKey => _config["YouTube:ApiKey"] ?? string.Empty;
    private string ChannelId => _config["YouTube:ChannelId"] ?? string.Empty;

    public bool IsConfigured => !string.IsNullOrEmpty(ApiKey);

    public YouTubeAnalyticsService(
        IHttpClientFactory httpClientFactory,
        IMemoryCache cache,
        IConfiguration config,
        ILogger<YouTubeAnalyticsService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _cache = cache;
        _config = config;
        _logger = logger;
    }

    public async Task<List<YouTubeVideoStats>> GetVideoStatsAsync(IEnumerable<string> videoIds)
    {
        if (!IsConfigured)
        {
            _logger.LogInformation("YouTube API not configured. Skipping video stats.");
            return [];
        }

        var ids = videoIds.ToList();
        if (ids.Count == 0) return [];

        var cacheKey = $"youtube-stats-{string.Join(",", ids.Order())}";
        if (_cache.TryGetValue(cacheKey, out List<YouTubeVideoStats>? cached) && cached is not null)
            return cached;

        try
        {
            var client = _httpClientFactory.CreateClient();
            var idParam = string.Join(",", ids);
            var url = $"https://www.googleapis.com/youtube/v3/videos?part=snippet,statistics,contentDetails&id={idParam}&key={ApiKey}";

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var results = ParseYouTubeResponse(json);

            _cache.Set(cacheKey, results, _cacheTtl);
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch YouTube video statistics.");
            return [];
        }
    }

    public async Task<List<YouTubeVideoStats>> GetChannelVideosAsync(int maxResults = 20)
    {
        if (!IsConfigured || string.IsNullOrEmpty(ChannelId))
        {
            _logger.LogInformation("YouTube API or ChannelId not configured.");
            return [];
        }

        var cacheKey = $"youtube-channel-{ChannelId}";
        if (_cache.TryGetValue(cacheKey, out List<YouTubeVideoStats>? cached) && cached is not null)
            return cached;

        try
        {
            var client = _httpClientFactory.CreateClient();
            var searchUrl = $"https://www.googleapis.com/youtube/v3/search?part=id&channelId={ChannelId}&type=video&maxResults={maxResults}&order=date&key={ApiKey}";

            var searchResponse = await client.GetAsync(searchUrl);
            searchResponse.EnsureSuccessStatusCode();

            var searchJson = await searchResponse.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(searchJson);
            var videoIds = doc.RootElement.GetProperty("items")
                .EnumerateArray()
                .Select(item => item.GetProperty("id").GetProperty("videoId").GetString() ?? string.Empty)
                .Where(id => !string.IsNullOrEmpty(id))
                .ToList();

            if (videoIds.Count == 0) return [];

            var results = await GetVideoStatsAsync(videoIds);
            _cache.Set(cacheKey, results, _cacheTtl);
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch channel videos.");
            return [];
        }
    }

    private static List<YouTubeVideoStats> ParseYouTubeResponse(string json)
    {
        var results = new List<YouTubeVideoStats>();

        using var doc = JsonDocument.Parse(json);
        if (!doc.RootElement.TryGetProperty("items", out var items)) return results;

        foreach (var item in items.EnumerateArray())
        {
            var snippet = item.GetProperty("snippet");
            var stats = item.GetProperty("statistics");
            var details = item.GetProperty("contentDetails");

            results.Add(new YouTubeVideoStats
            {
                VideoId = item.GetProperty("id").GetString() ?? string.Empty,
                Title = snippet.GetProperty("title").GetString() ?? string.Empty,
                ViewCount = long.TryParse(stats.GetProperty("viewCount").GetString(), out var vc) ? vc : 0,
                LikeCount = long.TryParse(stats.GetProperty("likeCount").GetString(), out var lc) ? lc : 0,
                CommentCount = long.TryParse(stats.GetProperty("commentCount").GetString(), out var cc) ? cc : 0,
                Duration = details.GetProperty("duration").GetString() ?? string.Empty
            });
        }

        return results;
    }
}
