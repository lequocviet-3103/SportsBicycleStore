using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Application.Interfaces.Services;
using SportsBicycleStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Infastructure.Services
{
    public class MProductService : IMProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Mproduct> CreateBicycle(ProductDto productDto)
        {
            return await _unitOfWork.MProductRepository.CreateBicycle(productDto);
        }
    }
}
