using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.Interfaces.Repositories
{
    public interface IMCategoryRepository
    {
        Task<Mcategory?> CreateCategoryAsync(CategoryDto dto);
        Task<Mcategory?> UpdateCategoryAsync(string categoryId, CategoryDto dto);
        Task<List<CategoryDto>> GetAllCategoriesAsync();
    }
}
