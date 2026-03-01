using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Application.Interfaces.Services;
using SportsBicycleStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Infastructure.Services
{
    public class MCategoryService : IMCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Mcategory?> CreateCategoryAsync(CategoryDto dto)
        {
            return await _unitOfWork.MCategoryRepository.CreateCategoryAsync(dto);
        }



        public async Task<Mcategory?> UpdateCategoryAsync(string categoryId, CategoryDto dto)
        {
            return await _unitOfWork.MCategoryRepository.UpdateCategoryAsync(categoryId, dto);
        }
        public async Task<List<GetAllCategoryDto>> GetAllCategoriesAsync()
        {
            return await _unitOfWork.MCategoryRepository.GetAllCategoriesAsync();
        }
    }
}