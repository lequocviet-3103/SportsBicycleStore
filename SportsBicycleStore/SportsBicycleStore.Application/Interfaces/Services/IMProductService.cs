using SportsBicycleStore.Application.DTO;
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
        Task<Mproduct?> GetMproductByIdAsync(string productId);
        Task<List<GetAllProductDto>> GetAllProductDtos();
        Task<GetAllProductDto?> GetProductById(string productId);
        Task<GetAllProductDto?> GetProductBySellerId(string sellerId);
    }
}
