using System;
using System.Collections.Generic;

namespace SportsBicycleStore.Domain.Entities;

public partial class InspectionReport
{
    public string ReportId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public string InspectorId { get; set; } = null!;

    public DateTime? InspectionDate { get; set; }

    public string? OverallRating { get; set; }

    public string? FrameCondition { get; set; }

    public string? FrameNotes { get; set; }

    public string? BrakeCondition { get; set; }

    public string? BrakeNotes { get; set; }

    public string? DrivetrainCondition { get; set; }

    public string? DrivetrainNotes { get; set; }

    public string? WheelCondition { get; set; }

    public string? WheelNotes { get; set; }

    public string? ImagesUrl { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public virtual User Inspector { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
