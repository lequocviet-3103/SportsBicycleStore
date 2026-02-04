using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SportsBicycleStore.Domain.Entities;
using System;
using System.Collections.Generic;


namespace SportsBicycleStore.Infastructure.Data.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<InspectionReport> InspectionReports { get; set; }

    public virtual DbSet<Listing> Listings { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Wishlist> Wishlists { get; set; }

    private string GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true).Build();
        return configuration["ConnectionStrings:DefaultConnection"];
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(GetConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("Brand_pkey");

            entity.ToTable("brand");

            entity.HasIndex(e => e.BrandName, "Brand_brand_name_key").IsUnique();

            entity.Property(e => e.BrandId)
                .HasMaxLength(40)
                .HasColumnName("brand_id");
            entity.Property(e => e.BrandName)
                .HasMaxLength(100)
                .HasColumnName("brand_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("Category_pkey");

            entity.ToTable("category");

            entity.Property(e => e.CategoryId)
                .HasMaxLength(40)
                .HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .HasColumnName("category_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<InspectionReport>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("InspectionReport_pkey");

            entity.ToTable("inspection_report");

            entity.Property(e => e.ReportId)
                .HasMaxLength(40)
                .HasColumnName("report_id");
            entity.Property(e => e.BrakeCondition)
                .HasMaxLength(20)
                .HasColumnName("brake_condition");
            entity.Property(e => e.BrakeNotes).HasColumnName("brake_notes");
            entity.Property(e => e.CompletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("completed_at");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DrivetrainCondition)
                .HasMaxLength(20)
                .HasColumnName("drivetrain_condition");
            entity.Property(e => e.DrivetrainNotes).HasColumnName("drivetrain_notes");
            entity.Property(e => e.FrameCondition)
                .HasMaxLength(20)
                .HasColumnName("frame_condition");
            entity.Property(e => e.FrameNotes).HasColumnName("frame_notes");
            entity.Property(e => e.ImagesUrl).HasColumnName("images_url");
            entity.Property(e => e.InspectionDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("inspection_date");
            entity.Property(e => e.InspectorId)
                .HasMaxLength(40)
                .HasColumnName("inspector_id");
            entity.Property(e => e.OverallRating)
                .HasMaxLength(10)
                .HasColumnName("overall_rating");
            entity.Property(e => e.ProductId)
                .HasMaxLength(40)
                .HasColumnName("product_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'pending'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.WheelCondition)
                .HasMaxLength(20)
                .HasColumnName("wheel_condition");
            entity.Property(e => e.WheelNotes).HasColumnName("wheel_notes");

            entity.HasOne(d => d.Inspector).WithMany(p => p.InspectionReports)
                .HasForeignKey(d => d.InspectorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_inspectionreport_inspector");

            entity.HasOne(d => d.Product).WithMany(p => p.InspectionReports)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_inspectionreport_product");
        });

        modelBuilder.Entity<Listing>(entity =>
        {
            entity.HasKey(e => e.ListingId).HasName("Listing_pkey");

            entity.ToTable("listing");

            entity.Property(e => e.ListingId)
                .HasMaxLength(40)
                .HasColumnName("listing_id");
            entity.Property(e => e.ApprovedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("approved_at");
            entity.Property(e => e.ApprovedBy)
                .HasMaxLength(40)
                .HasColumnName("approved_by");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpiredAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expired_at");
            entity.Property(e => e.FeaturedImage)
                .HasMaxLength(500)
                .HasColumnName("featured_image");
            entity.Property(e => e.ProductId)
                .HasMaxLength(40)
                .HasColumnName("product_id");
            entity.Property(e => e.SellerId)
                .HasMaxLength(40)
                .HasColumnName("seller_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'draft'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.ListingApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("fk_listing_approver");

            entity.HasOne(d => d.Product).WithMany(p => p.Listings)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_listing_product");

            entity.HasOne(d => d.Seller).WithMany(p => p.ListingSellers)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_listing_seller");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("Order_pkey");

            entity.ToTable("orders");

            entity.Property(e => e.OrderId)
                .HasMaxLength(40)
                .HasColumnName("order_id");
            entity.Property(e => e.BuyerId)
                .HasMaxLength(40)
                .HasColumnName("buyer_id");
            entity.Property(e => e.CancelledAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("cancelled_at");
            entity.Property(e => e.CompletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("completed_at");
            entity.Property(e => e.ConfirmedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("confirmed_at");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DeliveryMethod)
                .HasMaxLength(20)
                .HasColumnName("delivery_method");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(30)
                .HasDefaultValueSql("'pending'::character varying")
                .HasColumnName("order_status");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .HasDefaultValueSql("'unpaid'::character varying")
                .HasColumnName("payment_status");
            entity.Property(e => e.ReceiverName)
                .HasMaxLength(255)
                .HasColumnName("receiver_name");
            entity.Property(e => e.ReceiverPhone)
                .HasMaxLength(20)
                .HasColumnName("receiver_phone");
            entity.Property(e => e.SellerId)
                .HasMaxLength(40)
                .HasColumnName("seller_id");
            entity.Property(e => e.ShippingAddress)
                .HasMaxLength(500)
                .HasColumnName("shipping_address");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(18, 2)
                .HasColumnName("total_amount");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Buyer).WithMany(p => p.OrderBuyers)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_buyer");

            entity.HasOne(d => d.Seller).WithMany(p => p.OrderSellers)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_seller");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("OrderDetail_pkey");

            entity.ToTable("order_detail");

            entity.Property(e => e.OrderDetailId)
                .HasMaxLength(40)
                .HasColumnName("order_detail_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Note)
                .HasMaxLength(500)
                .HasColumnName("note");
            entity.Property(e => e.OrderId)
                .HasMaxLength(40)
                .HasColumnName("order_id");
            entity.Property(e => e.ProductId)
                .HasMaxLength(40)
                .HasColumnName("product_id");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.Subtotal)
                .HasPrecision(18, 2)
                .HasColumnName("subtotal");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(18, 2)
                .HasColumnName("unit_price");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("fk_orderdetail_order");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orderdetail_product");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("Payment_pkey");

            entity.ToTable("payment");

            entity.Property(e => e.PaymentId)
                .HasMaxLength(40)
                .HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasPrecision(18, 2)
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Note)
                .HasMaxLength(500)
                .HasColumnName("note");
            entity.Property(e => e.OrderId)
                .HasMaxLength(40)
                .HasColumnName("order_id");
            entity.Property(e => e.PaidAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("paid_at");
            entity.Property(e => e.PaymentGateway)
                .HasMaxLength(50)
                .HasColumnName("payment_gateway");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(30)
                .HasColumnName("payment_method");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .HasDefaultValueSql("'pending'::character varying")
                .HasColumnName("payment_status");
            entity.Property(e => e.PaymentType)
                .HasMaxLength(20)
                .HasColumnName("payment_type");
            entity.Property(e => e.RefundedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("refunded_at");
            entity.Property(e => e.TransactionCode)
                .HasMaxLength(100)
                .HasColumnName("transaction_code");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("fk_payment_order");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("Product_pkey");

            entity.ToTable("product");

            entity.Property(e => e.ProductId)
                .HasMaxLength(40)
                .HasColumnName("product_id");
            entity.Property(e => e.BrakeType)
                .HasMaxLength(50)
                .HasColumnName("brake_type");
            entity.Property(e => e.BrandId)
                .HasMaxLength(40)
                .HasColumnName("brand_id");
            entity.Property(e => e.CategoryId)
                .HasMaxLength(40)
                .HasColumnName("category_id");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .HasColumnName("color");
            entity.Property(e => e.Condition)
                .HasMaxLength(20)
                .HasColumnName("condition");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.FrameMaterial)
                .HasMaxLength(50)
                .HasColumnName("frame_material");
            entity.Property(e => e.FrameSize)
                .HasMaxLength(20)
                .HasColumnName("frame_size");
            entity.Property(e => e.GearSystem)
                .HasMaxLength(100)
                .HasColumnName("gear_system");
            entity.Property(e => e.InspectionStatus)
                .HasMaxLength(20)
                .HasDefaultValueSql("'Not Inspected'::character varying")
                .HasColumnName("inspection_status");
            entity.Property(e => e.LocationCity)
                .HasMaxLength(100)
                .HasColumnName("location_city");
            entity.Property(e => e.Price)
                .HasPrecision(18, 2)
                .HasColumnName("price");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .HasColumnName("product_name");
            entity.Property(e => e.PublishedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("published_at");
            entity.Property(e => e.SellerId)
                .HasMaxLength(40)
                .HasColumnName("seller_id");
            entity.Property(e => e.SoldAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("sold_at");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .HasDefaultValueSql("'pending_approval'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.StockQuantity)
                .HasDefaultValue(1)
                .HasColumnName("stock_quantity");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UsageHistory)
                .HasPrecision(10, 2)
                .HasColumnName("usage_history");
            entity.Property(e => e.ViewCount)
                .HasDefaultValue(0)
                .HasColumnName("view_count");
            entity.Property(e => e.Weight)
                .HasPrecision(5, 2)
                .HasColumnName("weight");
            entity.Property(e => e.WheelSize)
                .HasMaxLength(20)
                .HasColumnName("wheel_size");
            entity.Property(e => e.YearOfManufacture).HasColumnName("year_of_manufacture");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_brand");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_category");

            entity.HasOne(d => d.Seller).WithMany(p => p.Products)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_seller");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("role_pkey");

            entity.ToTable("role");

            entity.HasIndex(e => e.RoleName, "role_role_name_key").IsUnique();

            entity.Property(e => e.RoleId)
                .HasMaxLength(40)
                .HasColumnName("role_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("User_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "User_email_key").IsUnique();

            entity.HasIndex(e => e.UserName, "User_user_name_key").IsUnique();

            entity.Property(e => e.UserId)
                .HasMaxLength(40)
                .HasColumnName("user_id");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("address");
            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(500)
                .HasColumnName("avatar_url");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.RoleId)
                .HasMaxLength(40)
                .HasColumnName("role_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'active'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .HasColumnName("user_name");
        });

        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.HasKey(e => e.WishlistId).HasName("Wishlist_pkey");

            entity.ToTable("wishlist");

            entity.HasIndex(e => new { e.UserId, e.ProductId }, "uq_wishlist_user_product").IsUnique();

            entity.Property(e => e.WishlistId)
                .HasMaxLength(40)
                .HasColumnName("wishlist_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ListingId)
                .HasMaxLength(40)
                .HasColumnName("listing_id");
            entity.Property(e => e.Note)
                .HasMaxLength(500)
                .HasColumnName("note");
            entity.Property(e => e.ProductId)
                .HasMaxLength(40)
                .HasColumnName("product_id");
            entity.Property(e => e.UserId)
                .HasMaxLength(40)
                .HasColumnName("user_id");

            entity.HasOne(d => d.Listing).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.ListingId)
                .HasConstraintName("fk_wishlist_listing");

            entity.HasOne(d => d.Product).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("fk_wishlist_product");

            entity.HasOne(d => d.User).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_wishlist_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
