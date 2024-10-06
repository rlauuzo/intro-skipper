using System;
using System.Collections.Generic;

namespace ConfusedPolarBear.Plugin.IntroSkipper.Data;

/// <summary>
/// Episode queued for analysis.
/// </summary>
public class QueuedEpisode
{
    /// <summary>
    /// Gets or sets the series name.
    /// </summary>
    public string SeriesName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the season number.
    /// </summary>
    public int SeasonNumber { get; set; }

    /// <summary>
    /// Gets or sets the episode id.
    /// </summary>
    public Guid EpisodeId { get; set; }

    /// <summary>
    /// Gets or sets the series id.
    /// </summary>
    public Guid SeriesId { get; set; }

    /// <summary>
    /// Gets or sets the full path to episode.
    /// </summary>
    public string Path { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the episode.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether an episode is Anime.
    /// </summary>
    public bool IsAnime { get; set; }

    /// <summary>
    /// Gets or sets the timestamp (in seconds) to stop searching for an introduction at.
    /// </summary>
    public int IntroFingerprintEnd { get; set; }

    /// <summary>
    /// Gets or sets the timestamp (in seconds) to start looking for end credits at.
    /// </summary>
    public int CreditsFingerprintStart { get; set; }

    /// <summary>
    /// Gets or sets the total duration of this media file (in seconds).
    /// </summary>
    public int Duration { get; set; }

    /// <summary>
    /// Gets the episode information including segment statuses.
    /// </summary>
    public Dictionary<AnalysisMode, SegmentStatus> EpisodeInfo { get; } = [];

    /// <summary>
    /// Gets the status of a specific segment.
    /// </summary>
    /// <param name="mode">The mode of segment to get the status for.</param>
    /// <returns>The status of the specified segment.</returns>
    public SegmentStatus? GetSegmentStatus(AnalysisMode mode)
    {
        return (mode == AnalysisMode.Introduction ? Plugin.Instance!.Intros : Plugin.Instance!.Credits)
                .TryGetValue(EpisodeId, out var segment) ? (SegmentStatus)segment.Status : SegmentStatus.None;
    }

    /// <summary>
    /// Sets the status of a specific segment.
    /// </summary>
    /// <param name="mode">The mode of segment to set the status for.</param>
    /// <param name="status">The status to set.</param>
    public void SetSegmentStatus(AnalysisMode mode, SegmentStatus status)
    {
        (mode == AnalysisMode.Introduction ? Plugin.Instance!.Intros : Plugin.Instance!.Credits).AddOrUpdate(
            EpisodeId, _ => new Segment(EpisodeId, null, status), (_, segment) =>
            {
                segment.Status = (int)status;
                return segment;
            });
    }
}
