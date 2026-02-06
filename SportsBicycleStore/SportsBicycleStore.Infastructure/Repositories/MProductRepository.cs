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
    }
}
