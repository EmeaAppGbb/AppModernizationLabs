namespace Dashboard.Models;

public record ShareMetric
{
    public string LabName { get; init; } = string.Empty;
    public long ShareCount { get; init; }
    public long WebShareApiCount { get; init; }
    public long ClipboardCopyCount { get; init; }
}

public record ShareMetricSummary
{
    public List<ShareMetric> SharesByLab { get; init; } = [];
    public long TotalShares { get; init; }
    public long TotalWebShareApi { get; init; }
    public long TotalClipboardCopy { get; init; }
    public List<ShareConversion> ShareConversions { get; init; } = [];
}

public record ShareConversion
{
    public string LabName { get; init; } = string.Empty;
    public long Shares { get; init; }
    public long ResultingVisits { get; init; }
    public double ConversionRate => Shares > 0 ? (double)ResultingVisits / Shares * 100 : 0;
}
