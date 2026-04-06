namespace Dashboard.Models;

public record LabEngagementMetric
{
    public string LabName { get; init; } = string.Empty;
    public long ClickCount { get; init; }
    public long CloneClicks { get; init; }
    public long VsCodeClicks { get; init; }
    public long CodespaceClicks { get; init; }
    public long StarClicks { get; init; }
    public long StartMenuInteractions { get; init; }
    public long FilterUsageCount { get; init; }
}

public record LabEngagementSummary
{
    public List<LabEngagementMetric> TopLabs { get; init; } = [];
    public long TotalCloneClicks { get; init; }
    public long TotalVsCodeClicks { get; init; }
    public long TotalCodespaceClicks { get; init; }
    public long TotalStartMenuInteractions { get; init; }
    public long TotalStarClicks { get; init; }
    public List<FilterUsageMetric> FilterUsagePatterns { get; init; } = [];
}

public record FilterUsageMetric
{
    public string FilterName { get; init; } = string.Empty;
    public string FilterValue { get; init; } = string.Empty;
    public long UsageCount { get; init; }
}
