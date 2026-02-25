using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.Interfaces.Repositories
{
    public interface IListingRepository
    {
        Task<Mlisting> CreateListing(ListingDto listingDto);
        Task<Mlisting?> GetByIdAsync(string id);
        Task<bool> ApproveListing(ApproveListingDto approveListingDto, string listingId);
        Task<List<GetAllListing>> GetAllListings();
    }
}
