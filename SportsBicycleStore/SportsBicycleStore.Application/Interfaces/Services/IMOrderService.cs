using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.Interfaces.Services
{
    public interface IMOrderService
    {
        Task<Morder> CreateOrder(OrderDto orderDto);
        Task<Morder?> GetOrderByIdAsync(string orderId);
        Task<List<GetAllOrderDto>> GetAllOrderDtos();
        Task<GetAllOrderDto?> GetOrderById(string orderId);
        Task<List<GetAllOrderDto>> GetOrderByUserId(string userId);
        Task<List<GetAllOrderDto>> GetOrderBySellerId(string sellerId);
    }
}
