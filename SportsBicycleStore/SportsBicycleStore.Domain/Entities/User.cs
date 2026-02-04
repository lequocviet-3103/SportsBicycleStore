using System;
using System.Collections.Generic;

namespace SportsBicycleStore.Domain.Entities;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? FullName { get; set; }

    public string? AvatarUrl { get; set; }

    public string? Address { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Status { get; set; }

    public string RoleId { get; set; } = null!;

    public virtual ICollection<InspectionReport> InspectionReports { get; set; } = new List<InspectionReport>();

    public virtual ICollection<Listing> ListingApprovedByNavigations { get; set; } = new List<Listing>();

    public virtual ICollection<Listing> ListingSellers { get; set; } = new List<Listing>();

    public virtual ICollection<Order> OrderBuyers { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderSellers { get; set; } = new List<Order>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
