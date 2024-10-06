namespace ConfusedPolarBear.Plugin.IntroSkipper.Data;

/// <summary>
/// Status of segment analysis.
/// </summary>
public enum SegmentStatus
{
    /// <summary>
    /// Default / Wasn't scanned.
    /// </summary>
    None = 0,

    /// <summary>
    /// Scanned and segment found.
    /// </summary>
    SegmentFound = 1,

    /// <summary>
    /// Scanned but no segment found.
    /// </summary>
    NoSegmentFound = 2,

    /// <summary>
    /// Don't scan.
    /// </summary>
    DoNotScan = 3
}
