using Microsoft.EntityFrameworkCore;
using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Extension;
using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Application.SearchFilter;
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
        
        public async Task<PagedResult<Mproduct>> GetProductsAsync(ProductSearchFilter filter)
        {
            var query = _context.Mproducts
                .Include(p => p.Category)
                .Include(p => p.Brand)
                //.Include(p => p.Seller)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.ProductId))
            {
                query = query.Where(p => p.ProductId.Contains(filter.ProductId));
            }

            if (!string.IsNullOrEmpty(filter.ProductName))
            {
                query = query.Where(p => p.ProductName.Contains(filter.ProductName));
            }

            if (!string.IsNullOrEmpty(filter.SellerId))
            {
                query = query.Where(p => p.SellerId == filter.SellerId);
            }

            if (!string.IsNullOrEmpty(filter.CategoryId))
            {
                query = query.Where(p => p.CategoryId == filter.CategoryId);
            }

            if (!string.IsNullOrEmpty(filter.BrandId))
            {
                query = query.Where(p => p.BrandId == filter.BrandId);
            }

            if (filter.Status.HasValue)
            {
                query = query.Where(p => p.Status == filter.Status.Value);
            }

            if (filter.Condition.HasValue)
            {
                query = query.Where(p => p.Condition == filter.Condition.Value);
            }

            if (filter.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new PagedResult<Mproduct>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };
        }
    }
}
