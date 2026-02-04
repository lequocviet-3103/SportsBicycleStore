using System;
using System.Collections.Generic;

namespace SportsBicycleStore.Domain.Entities;

public partial class Product
{
    public string ProductId { get; set; } = null!;

    public string SellerId { get; set; } = null!;

    public string CategoryId { get; set; } = null!;

    public string BrandId { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    public string? Condition { get; set; }

    public string? FrameSize { get; set; }

    public string? FrameMaterial { get; set; }

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

    public int? ViewCount { get; set; }

    public string? Status { get; set; }

    public string? InspectionStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? PublishedAt { get; set; }

    public DateTime? SoldAt { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<InspectionReport> InspectionReports { get; set; } = new List<InspectionReport>();

    public virtual ICollection<Listing> Listings { get; set; } = new List<Listing>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User Seller { get; set; } = null!;

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
