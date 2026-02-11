using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Application.Interfaces.Services;
using SportsBicycleStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Infastructure.Services
{
    public class MlistingService : IListingService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MlistingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool?> ApproveListing(ApproveListingDto approveListingDto, string listingId)
        {
            
            await _unitOfWork.MlistingRepository.ApproveListing(approveListingDto, listingId);
            return true;
        }

        public async Task<Mlisting> CreateListing(ListingDto listingDto)
        {
            return await _unitOfWork.MlistingRepository.CreateListing(listingDto);
        }

        public async Task<Mlisting?> GetByIdAsync(string id)
        {
            return await _unitOfWork.MlistingRepository.GetByIdAsync(id);
        }
    }
}
