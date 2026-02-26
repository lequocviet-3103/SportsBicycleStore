using Microsoft.EntityFrameworkCore;
using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Domain.Entities;
using SportsBicycleStore.Domain.Enum;
using SportsBicycleStore.Infastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Infastructure.Repositories
{
    public class MProductRepository : Repository<Mproduct> , IMProductRepository
    {
        public MProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Mproduct> CreateBicycle(ProductDto productDto)
        {
            var bicycle = new Mproduct
            {
                ProductId = Guid.NewGuid().ToString(),
                SellerId = productDto.SellerId,
                CategoryId = productDto.CategoryId,
                BrandId = productDto.BrandId,
                ProductName = productDto.ProductName,
                Description = productDto.Description,
                Condition = productDto.Condition,
                FrameSize   = productDto.FrameSize,
                FrameMaterial = productDto.FrameMaterial,
                WheelSize = productDto.WheelSize,
                BrakeType = productDto.BrakeType,
                GearSystem = productDto.GearSystem,
                Weight = productDto.Weight,
                Color = productDto.Color,
                YearOfManufacture = productDto.YearOfManufacture,
                UsageHistory = productDto.UsageHistory,
                Price = productDto.Price,
                StockQuantity = productDto.StockQuantity,
                LocationCity = productDto.LocationCity,
                Status = (int)ProductStatus.Available,
                InspectionStatus = (int)InspectionStatus.Not_Inspectioned,
                //CreatedAt = DateTime.UtcNow
            };
            await _context.Mproducts.AddAsync(bicycle);
            await _context.SaveChangesAsync();
            return bicycle;
        }

        public async Task<List<GetAllProductDto>> GetAllProductDtos()
        {
            var products = await _context.Mproducts
                .Include(p => p.Seller)
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Select(p => new GetAllProductDto
                {
                    ProductId = p.ProductId,
                    SellerId = p.SellerId,
                    SellerName = p.Seller.UserName,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.CategoryName,
                    BrandId = p.BrandId,
                    BrandName = p.Brand.BrandName,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    Condition = p.Condition,
                    FrameSize = p.FrameSize,
                    FrameMaterial = p.FrameMaterial,
                    WheelSize = p.WheelSize,
                    BrakeType = p.BrakeType,
                    GearSystem = p.GearSystem,
                    Weight = p.Weight,
                    Color = p.Color,
                    YearOfManufacture = p.YearOfManufacture,
                    UsageHistory = p.UsageHistory,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    LocationCity = p.LocationCity,
                    Status = (int)(ProductStatus)p.Status!,
                    InspectionStatus = (int)(InspectionStatus)p.InspectionStatus!,
                    ViewCount = p.ViewCount,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    PublishedAt = p.PublishedAt,
                    SoldAt  = p.SoldAt
                }).ToListAsync();

            return products;
        }

        public Task<Mproduct?> GetMproductByIdAsync(string productId)
        {
            var product = _context.Mproducts.FirstOrDefaultAsync(p => p.ProductId == productId);
            return product;
        }

        public async Task<GetAllProductDto?> GetProductById(string productId)
        {
            var product = await _context.Mproducts
                .Include(p => p.Seller)
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Where(p => p.ProductId == productId)
                .Select(p => new GetAllProductDto
                {
                    ProductId = p.ProductId,
                    SellerId = p.SellerId,
                    SellerName = p.Seller.UserName,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.CategoryName,
                    BrandId = p.BrandId,
                    BrandName = p.Brand.BrandName,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    Condition = p.Condition,
                    FrameSize = p.FrameSize,
                    FrameMaterial = p.FrameMaterial,
                    WheelSize = p.WheelSize,
                    BrakeType = p.BrakeType,
                    GearSystem = p.GearSystem,
                    Weight = p.Weight,
                    Color = p.Color,
                    YearOfManufacture = p.YearOfManufacture,
                    UsageHistory = p.UsageHistory,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    LocationCity = p.LocationCity,
                    Status = (int)(ProductStatus)p.Status!,
                    InspectionStatus = (int)(InspectionStatus)p.InspectionStatus!,
                    ViewCount = p.ViewCount,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    PublishedAt = p.PublishedAt
                }).FirstOrDefaultAsync();
            return product;
        }

        public async Task<GetAllProductDto?> GetProductBySellerId(string sellerId)
        {
            var seller = await _context.Mproducts
                .Include(p => p.Seller)
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Where(p => p.SellerId == sellerId)
                .Select(p => new GetAllProductDto
                {
                    ProductId = p.ProductId,
                    SellerId = p.SellerId,
                    SellerName = p.Seller.UserName,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.CategoryName,
                    BrandId = p.BrandId,
                    BrandName = p.Brand.BrandName,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    Condition = p.Condition,
                    FrameSize = p.FrameSize,
                    FrameMaterial = p.FrameMaterial,
                    WheelSize = p.WheelSize,
                    BrakeType = p.BrakeType,
                    GearSystem = p.GearSystem,
                    Weight = p.Weight,
                    Color = p.Color,
                    YearOfManufacture = p.YearOfManufacture,
                    UsageHistory = p.UsageHistory,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    LocationCity = p.LocationCity,
                    Status = (int)(ProductStatus)p.Status!,
                    InspectionStatus = (int)(InspectionStatus)p.InspectionStatus!,
                    ViewCount = p.ViewCount,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    PublishedAt = p.PublishedAt
                }).FirstOrDefaultAsync();
            return seller;
        }

        public async Task<Mproduct?> UpdateBicycleAsync(string productId, UpdateProductDto dto)
        {
            var product = await _context.Mproducts.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (product == null)
            {
                return null;
            }

            // Validate Foreign Keys
            // Check SellerId 
            bool sellerExists = await _context.Musers.AnyAsync(u => u.UserId == dto.SellerId);
            if (!sellerExists)
            {
                throw new SportsBicycleStore.Application.Exceptions.UserFriendlyException(
                    400,
                    "SELLER_NOT_FOUND",
                    "Seller ID does not exist.");
            }

            // Check CategoryId
            bool categoryExists = await _context.Mcategories.AnyAsync(c => c.CategoryId == dto.CategoryId);
            if (!categoryExists)
            {
                throw new SportsBicycleStore.Application.Exceptions.UserFriendlyException(
                    400,
                    "CATEGORY_NOT_FOUND",
                    "Category ID does not exist.");
            }

            // Check BrandId
            bool brandExists = await _context.Mbrands.AnyAsync(b => b.BrandId == dto.BrandId);
            if (!brandExists)
            {
                throw new SportsBicycleStore.Application.Exceptions.UserFriendlyException(
                    400,
                    "BRAND_NOT_FOUND",
                    "Brand ID does not exist.");
            }

            product.SellerId = dto.SellerId;
            product.CategoryId = dto.CategoryId;
            product.BrandId = dto.BrandId;
            product.ProductName = dto.ProductName;
            product.Description = dto.Description;
            product.Condition = dto.Condition;
            product.FrameSize = dto.FrameSize;
            product.FrameMaterial = dto.FrameMaterial;
            product.WheelSize = dto.WheelSize;
            product.BrakeType = dto.BrakeType;
            product.GearSystem = dto.GearSystem;
            product.Weight = dto.Weight;
            product.Color = dto.Color;
            product.YearOfManufacture = dto.YearOfManufacture;
            product.UsageHistory = dto.UsageHistory;
            product.Price = dto.Price;
            product.StockQuantity = dto.StockQuantity;
            product.LocationCity = dto.LocationCity;
            product.Status = dto.Status;
            product.InspectionStatus = dto.InspectionStatus;
            product.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return product;
        }
    }
}
