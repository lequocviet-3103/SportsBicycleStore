using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.DTO
{
    public class UpdateInspectionReportDto
    {
        //public string ProductId { get; set; } = null!;

        //public string InspectorId { get; set; } = null!;

        //public DateTime? InspectionDate { get; set; }

        public string? OverallRating { get; set; }

        public int? FrameCondition { get; set; }

        public string? FrameNotes { get; set; }

        public int? BrakeCondition { get; set; }

        public string? BrakeNotes { get; set; }

        public int? DrivetrainCondition { get; set; }

        public string? DrivetrainNotes { get; set; }

        public int? WheelCondition { get; set; }

        public string? WheelNotes { get; set; }

        //public string? ImagesUrl { get; set; }

        public int? Status { get; set; }

        //public DateTime? CompletedAt { get; set; }
    }
}
