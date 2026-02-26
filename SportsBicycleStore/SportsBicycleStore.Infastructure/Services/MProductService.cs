using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Exceptions;
using SportsBicycleStore.Application.Extension;
using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Application.Interfaces.Services;
using SportsBicycleStore.Application.SearchFilter;
using SportsBicycleStore.Domain.Entities;
using SportsBicycleStore.Domain.Enum;
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

        

        public async Task<Mproduct?> UpdateBicycleAsync(string productId, UpdateProductDto dto)
        {
            // Validate SellerId
            var seller = await _unitOfWork.UserRepository
                .GetByUserIdAsync(dto.SellerId);

            if (seller == null)
            {
                throw new UserFriendlyException(
                    400,
                    "SELLER_NOT_FOUND",
                    "Seller ID does not exist.");
            }

            if (dto.Condition.HasValue &&
        !Enum.IsDefined(typeof(ProductCondition), dto.Condition.Value))
            {
                throw new UserFriendlyException(
                    400,
                    "INVALID_CONDITION",
                    "Condition must be 1 (New), 2 (Used) or 3 (Refurbished)."
                );
            }

            if (dto.FrameMaterial.HasValue &&
                !Enum.IsDefined(typeof(FrameMaterial), dto.FrameMaterial.Value))
            {
                throw new UserFriendlyException(
                    400,
                    "INVALID_FRAME_MATERIAL",
                    "FrameMaterial must be from 1 to 5."
                );
            }

            if (dto.Status.HasValue &&
                !Enum.IsDefined(typeof(ProductStatus), dto.Status.Value))
            {
                throw new UserFriendlyException(
                    400,
                    "INVALID_STATUS",
                    "Status must be from 1 to 6."
                );
            }

            if (dto.InspectionStatus.HasValue &&
                !Enum.IsDefined(typeof(InspectionStatus), dto.InspectionStatus.Value))
            {
                throw new UserFriendlyException(
                    400,
                    "INVALID_INSPECTION_STATUS",
                    "InspectionStatus must be 1 or 2."
                );
            }
            return await _unitOfWork.MProductRepository.UpdateBicycleAsync(productId, dto);
        }
        public async Task<PagedResult<Mproduct>> GetProductsAsync(ProductSearchFilter filter)
        {
            return await _unitOfWork.MProductRepository.GetProductsAsync(filter);
        }
    }
}
