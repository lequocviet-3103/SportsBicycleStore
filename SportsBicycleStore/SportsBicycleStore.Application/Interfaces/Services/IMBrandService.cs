using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.Interfaces.Services
{
    public interface IMBrandService
    {
        Task<Mbrand?> CreateBrandAsync(BrandDto dto);
        Task<Mbrand?> UpdateBrandAsync(string brandId, BrandDto dto);
        Task<List<GetAllBrandDto>> GetAllBrandsAsync();
    }
}