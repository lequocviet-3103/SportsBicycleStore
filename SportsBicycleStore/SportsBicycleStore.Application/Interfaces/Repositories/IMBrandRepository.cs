using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.Interfaces.Repositories
{
    public interface IMBrandRepository
    {
        Task<Mbrand?> CreateBrandAsync(BrandDto dto);
        Task<Mbrand?> UpdateBrandAsync(string brandId, BrandDto dto);
        Task<List<BrandDto>> GetAllBrandsAsync();
    }
}
