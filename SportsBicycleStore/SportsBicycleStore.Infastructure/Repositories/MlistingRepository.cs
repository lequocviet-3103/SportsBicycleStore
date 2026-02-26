using Microsoft.EntityFrameworkCore;
using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Domain.Entities;
using SportsBicycleStore.Infastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Infastructure.Repositories
{
    public class MlistingRepository : Repository<Mlisting>, IListingRepository
    {
        public MlistingRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> ApproveListing(ApproveListingDto approveListingDto, string listingId)
        {
            var listing = await _context.Mlistings.FirstOrDefaultAsync(l => l.ListingId == listingId);
            if (listing == null)
            {
                return false;
            }
            listing.Status = 2;
            listing.ApprovedBy = approveListingDto.ApprovedBy;
            listing.ApprovedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Mlisting> CreateListing(ListingDto listingDto)
        {
            var listing = new Mlisting
            {
                ListingId = Guid.NewGuid().ToString(),
                ProductId = listingDto.ProductId!,
                SellerId = listingDto.SellerId!,
                Title = listingDto.Title!,
                FeaturedImage = listingDto.FeaturedImage,
                Status = 1,
                CreatedAt = DateTime.Now
            };
            await _context.Mlistings.AddAsync(listing);
            await _context.SaveChangesAsync();
            return listing;
        }

        public async Task<Mlisting?> GetByIdAsync(string id)
        {
            var listing = await _context.Mlistings.FirstOrDefaultAsync(l => l.ListingId == id);
            return listing;
        }
    }
}
