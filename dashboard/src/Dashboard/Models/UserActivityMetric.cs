namespace Dashboard.Models;

public record UserActivityMetric
{
    public DateTime Timestamp { get; init; }
    public long SessionCount { get; init; }
    public double AvgSessionDurationSeconds { get; init; }
    public long NewUsers { get; init; }
    public long ReturningUsers { get; init; }
}

public record UserActivitySummary
{
    public List<UserActivityMetric> SessionsOverTime { get; init; } = [];
    public double OverallAvgSessionDuration { get; init; }
    public long TotalNewUsers { get; init; }
    public long TotalReturningUsers { get; init; }
    public long DarkThemeUsers { get; init; }
    public long LightThemeUsers { get; init; }
    public List<SearchQueryMetric> TopSearchQueries { get; init; } = [];
}

public record SearchQueryMetric
{
    public string Query { get; init; } = string.Empty;
    public long Count { get; init; }
}
