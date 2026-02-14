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
    }
}
