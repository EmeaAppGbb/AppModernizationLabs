namespace Dashboard.Models;

public record VideoMetric
{
    public string LabName { get; init; } = string.Empty;
    public string VideoId { get; init; } = string.Empty;
    public string VideoTitle { get; init; } = string.Empty;
    public long PlayCount { get; init; }
    public long PauseCount { get; init; }
    public long CompleteCount { get; init; }
    public double AvgWatchTimeSeconds { get; init; }
}

public record VideoMetricSummary
{
    public List<VideoMetric> VideosByLab { get; init; } = [];
    public long TotalPlays { get; init; }
    public long TotalCompletes { get; init; }
    public double OverallAvgWatchTime { get; init; }
    public List<VideoMetric> MostWatched { get; init; } = [];
}

public record YouTubeVideoStats
{
    public string VideoId { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public long ViewCount { get; init; }
    public long LikeCount { get; init; }
    public long CommentCount { get; init; }
    public string Duration { get; init; } = string.Empty;
}
