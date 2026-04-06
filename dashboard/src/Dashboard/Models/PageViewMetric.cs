namespace Dashboard.Models;

public record PageViewMetric
{
    public DateTime Timestamp { get; init; }
    public string PageName { get; init; } = string.Empty;
    public string PageUrl { get; init; } = string.Empty;
    public long ViewCount { get; init; }
    public long UniqueUsers { get; init; }
    public string? Country { get; init; }
    public string? City { get; init; }
    public string? Browser { get; init; }
    public string? OperatingSystem { get; init; }
    public string? DeviceType { get; init; }
}

public record PageViewSummary
{
    public long TotalViewsToday { get; init; }
    public long TotalViews7Days { get; init; }
    public long TotalViews30Days { get; init; }
    public long TotalUniqueUsers { get; init; }
    public long ActiveUsersNow { get; init; }
    public List<PageViewMetric> TopPages { get; init; } = [];
    public List<PageViewMetric> ViewsOverTime { get; init; } = [];
    public List<GeoMetric> GeographicDistribution { get; init; } = [];
    public List<DeviceMetric> DeviceBreakdown { get; init; } = [];
}

public record GeoMetric
{
    public string Country { get; init; } = string.Empty;
    public long Count { get; init; }
}

public record DeviceMetric
{
    public string Name { get; init; } = string.Empty;
    public long Count { get; init; }
}
