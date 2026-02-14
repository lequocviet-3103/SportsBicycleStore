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
    }
}
