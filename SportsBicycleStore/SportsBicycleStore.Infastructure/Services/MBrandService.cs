using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Application.Interfaces.Services;
using SportsBicycleStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Infastructure.Services
{
    public class MBrandService : IMBrandService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MBrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Mbrand?> CreateBrandAsync(BrandDto dto)
        {
            return await _unitOfWork.MBrandRepository.CreateBrandAsync(dto);
        }

        public async Task<List<GetAllBrandDto>> GetAllBrandsAsync()
        {
            return await _unitOfWork.MBrandRepository.GetAllBrandsAsync();
        }

        public async Task<Mbrand?> UpdateBrandAsync(string brandId, BrandDto dto)
        {
            return await _unitOfWork.MBrandRepository.UpdateBrandAsync(brandId, dto);
        }
    }
}