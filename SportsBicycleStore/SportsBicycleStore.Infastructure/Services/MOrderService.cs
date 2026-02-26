using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Application.Interfaces.Services;
using SportsBicycleStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Infastructure.Services
{
    public class MOrderService : IMOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MOrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Morder> CreateOrder(OrderDto orderDto)
        {
            return await _unitOfWork.MOrderRepository.CreateOrder(orderDto);
        }

        public async Task<List<GetAllOrderDto>> GetAllOrderDtos()
        {
            return await _unitOfWork.MOrderRepository.GetAllOrderDtos();
        }

        public async Task<GetAllOrderDto?> GetOrderById(string orderId)
        {
            return await _unitOfWork.MOrderRepository.GetOrderById(orderId);
        }

        public async Task<Morder?> GetOrderByIdAsync(string orderId)
        {
            return await _unitOfWork.MOrderRepository.GetOrderByIdAsync(orderId);
        }

        public async Task<GetAllOrderDto?> GetOrderByUserId(string userId)
        {
            return await _unitOfWork.MOrderRepository.GetOrderByUserId(userId);
        }
    }
}
