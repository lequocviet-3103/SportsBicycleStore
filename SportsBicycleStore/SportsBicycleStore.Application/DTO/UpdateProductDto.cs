using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.DTO
{
    public class UpdateProductDto
    {
        public string SellerId { get; set; } = null!;

        public string CategoryId { get; set; } = null!;

        public string BrandId { get; set; } = null!;

        public string ProductName { get; set; } = null!;

        public string? Description { get; set; }

        public int? Condition { get; set; }

        public string? FrameSize { get; set; }

        public int? FrameMaterial { get; set; }

        public string? WheelSize { get; set; }

        public string? BrakeType { get; set; }

        public string? GearSystem { get; set; }

        public decimal? Weight { get; set; }

        public string? Color { get; set; }

        public int? YearOfManufacture { get; set; }

        public decimal? UsageHistory { get; set; }

        public decimal Price { get; set; }

        public int? StockQuantity { get; set; }

        public string? LocationCity { get; set; }

        public int? Status { get; set; }

        public int? InspectionStatus { get; set; }
    }
}
