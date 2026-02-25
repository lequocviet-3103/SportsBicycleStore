using Microsoft.EntityFrameworkCore;
using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Domain.Entities;
using SportsBicycleStore.Infastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Infastructure.Repositories
{
    public class MBrandRepository : Repository<Mbrand>, IMBrandRepository
    {
        public MBrandRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Mbrand?> CreateBrandAsync(BrandDto dto)
        {
            var brand = new Mbrand
            {
                BrandId = Guid.NewGuid().ToString(),
                BrandName = dto.BrandName,
                Description = dto.Description,
                IsActive = dto.IsActive ?? true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _context.Mbrands.AddAsync(brand);
            await _context.SaveChangesAsync();
            return brand;
        }

        public async Task<List<BrandDto>> GetAllBrandsAsync()
        {
            return await _context.Mbrands
                .Select(b => new BrandDto
                {
                    BrandName = b.BrandName,
                    Description = b.Description,
                    IsActive = b.IsActive
                })
                .ToListAsync();
        }

        public async Task<Mbrand?> UpdateBrandAsync(string brandId,BrandDto dto)
        {
            var brand = await _context.Mbrands
        .FirstOrDefaultAsync(b => b.BrandId == brandId);

            if (brand == null)
            {
                return null;
            }

            brand.BrandName = dto.BrandName;
            brand.Description = dto.Description;
            brand.IsActive = dto.IsActive ?? brand.IsActive;
            brand.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return brand;
        }
    }
}
