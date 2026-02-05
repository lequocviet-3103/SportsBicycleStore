using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.Interfaces.Repositories
{
    public interface IMProductRepository
    {
        public Task<Mproduct> CreateBicycle(ProductDto productDto);
    }
}
