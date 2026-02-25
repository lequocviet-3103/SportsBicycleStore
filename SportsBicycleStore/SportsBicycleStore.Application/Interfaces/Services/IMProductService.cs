using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Extension;
using SportsBicycleStore.Application.SearchFilter;
using SportsBicycleStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.Interfaces.Services
{
    public interface IMProductService
    {
        public Task<Mproduct> CreateBicycle(ProductDto productDto);
        Task<Mproduct?> UpdateBicycleAsync(string productId, UpdateProductDto dto);
        Task<PagedResult<Mproduct>> GetProductsAsync(ProductSearchFilter filter);

    }
}
