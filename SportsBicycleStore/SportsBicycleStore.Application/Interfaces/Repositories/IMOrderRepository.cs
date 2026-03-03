using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Domain.Entities;
using SportsBicycleStore.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.Interfaces.Repositories
{
    public interface IMOrderRepository
    {
        Task<Morder> CreateOrder(OrderDto orderDto);
        Task<Morder?> GetOrderByIdAsync(string orderId);
        Morder UpdateOrderStatus(string orderId, OrderStatus newStatus);
        Morder UpdateOrderPaymentStatus(string orderId, OrderPaymentStatus newStatus);
        Task<List<GetAllOrderDto>> GetAllOrderDtos();
        Task<GetAllOrderDto?> GetOrderById(string orderId);
        Task<List<GetAllOrderDto>> GetOrderByUserId(string userId);
        Task<List<GetAllOrderDto>> GetOrderBySellerId(string sellerId);
    }
}
