using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.DTO
{
    public class InspectionReportDto
    {
        public string ProductId { get; set; } = null!;

        public string InspectorId { get; set; } = null!;

        public string? ImagesUrl { get; set; }
    }
}
