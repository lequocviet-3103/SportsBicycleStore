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
    public class MCategoryRepository : Repository<Mcategory>, IMCategoryRepository
    {
        public MCategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Mcategory?> CreateCategoryAsync(CategoryDto dto)
        {
            var category = new Mcategory
            {
                CategoryId = Guid.NewGuid().ToString(),
                CategoryName = dto.CategoryName,
                Description = dto.Description,
                IsActive = dto.IsActive ?? true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _context.Mcategories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }



        public async Task<Mcategory?> UpdateCategoryAsync(string categoryId, CategoryDto dto)
        {
            var category = await _context.Mcategories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
            if (category == null)
            {
                return null;
            }
            category.CategoryName = dto.CategoryName;
            category.Description = dto.Description;
            category.IsActive = dto.IsActive ?? category.IsActive;
            category.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return category;
        }
        public async Task<List<GetAllCategoryDto>> GetAllCategoriesAsync()
        {
            return await _context.Mcategories
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new GetAllCategoryDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    Description = c.Description,
                    IsActive = c.IsActive
                })
                .ToListAsync();
        }
    }
}