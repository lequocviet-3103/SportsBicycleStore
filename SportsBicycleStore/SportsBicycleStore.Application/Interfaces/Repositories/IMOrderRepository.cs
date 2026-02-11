using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.Interfaces.Repositories
{
    public interface IMOrderRepository
    {
        Task<Morder> CreateOrder(OrderDto orderDto);
    }
}
