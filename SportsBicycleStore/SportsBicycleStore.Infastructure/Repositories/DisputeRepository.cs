using Microsoft.EntityFrameworkCore;
using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Domain.Entities;
using SportsBicycleStore.Domain.Enum;
using SportsBicycleStore.Infastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Infastructure.Repositories
{
    public class DisputeRepository : Repository<Mdispute>, IDisputeRepository
    {
        public DisputeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Mdispute> CreateDispute(DisputeDto disputeDto)
        {
            // Validate order exists
            var order = await _context.Morders.FirstOrDefaultAsync(o => o.OrderId == disputeDto.OrderId);
            if (order == null)
                throw new Exception("Order not found");

            // Check if order is in a valid state for dispute (Paid or Completed)
            var orderStatus = (OrderStatus)order.OrderStatus!;
            if (orderStatus != OrderStatus.Paid && orderStatus != OrderStatus.Completed)
                throw new Exception($"Cannot create dispute for order with status {orderStatus}. Order must be Paid or Completed.");

            // Check if a dispute already exists for this order
            var existingDispute = await _context.Mdisputes
                .FirstOrDefaultAsync(d => d.OrderId == disputeDto.OrderId
                    && d.Status != (int)DisputeStatus.Closed
                    && d.Status != (int)DisputeStatus.Resolved);
            if (existingDispute != null)
                throw new Exception("An active dispute already exists for this order");

            // Validate buyer
            if (disputeDto.BuyerId != order.BuyerId)
                throw new Exception("Only the buyer of this order can create a dispute");

            var dispute = new Mdispute
            {
                DisputeId = Guid.NewGuid().ToString(),
                OrderId = disputeDto.OrderId!,
                BuyerId = order.BuyerId,
                SellerId = order.SellerId,
                Reason = disputeDto.Reason,
                Description = disputeDto.Description!,
                EvidenceUrls = disputeDto.EvidenceUrls,
                Status = (int)DisputeStatus.Open,
                CreatedAt = DateTime.Now
            };

            await _context.Mdisputes.AddAsync(dispute);

            // Update order status to Disputed
            order.OrderStatus = (int)OrderStatus.Disputed;
            order.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return dispute;
        }

        public async Task<Mdispute?> GetDisputeByIdAsync(string disputeId)
        {
            return await _context.Mdisputes
                .Include(d => d.Order)
                .Include(d => d.Buyer)
                .Include(d => d.Seller)
                .Include(d => d.ResolvedByNavigation)
                .Include(d => d.MdisputeEvidences)
                .FirstOrDefaultAsync(d => d.DisputeId == disputeId);
        }

        public async Task<Mdispute?> GetDisputeByOrderIdAsync(string orderId)
        {
            return await _context.Mdisputes
                .Include(d => d.Order)
                .Include(d => d.Buyer)
                .Include(d => d.Seller)
                .Include(d => d.ResolvedByNavigation)
                .Include(d => d.MdisputeEvidences)
                .FirstOrDefaultAsync(d => d.OrderId == orderId);
        }

        public async Task<List<Mdispute>> GetAllDisputesAsync()
        {
            return await _context.Mdisputes
                .Include(d => d.Order)
                .Include(d => d.Buyer)
                .Include(d => d.Seller)
                .Include(d => d.ResolvedByNavigation)
                .Include(d => d.MdisputeEvidences)
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
        }

        public async Task<MdisputeEvidence> AddEvidence(DisputeEvidenceDto evidenceDto)
        {
            var dispute = await _context.Mdisputes.FirstOrDefaultAsync(d => d.DisputeId == evidenceDto.DisputeId);
            if (dispute == null)
                throw new Exception("Dispute not found");

            if (dispute.Status == (int)DisputeStatus.Resolved || dispute.Status == (int)DisputeStatus.Closed)
                throw new Exception("Cannot add evidence to a resolved or closed dispute");

            var evidence = new MdisputeEvidence
            {
                EvidenceId = Guid.NewGuid().ToString(),
                DisputeId = evidenceDto.DisputeId!,
                SubmittedBy = evidenceDto.SubmittedBy!,
                ImageUrl = evidenceDto.ImageUrl,
                VideoUrl = evidenceDto.VideoUrl,
                Description = evidenceDto.Description,
                CreatedAt = DateTime.Now
            };

            await _context.MdisputeEvidences.AddAsync(evidence);
            
            // Update dispute status if it was waiting for evidence
            if (dispute.Status == (int)DisputeStatus.WaitingForEvidence)
            {
                dispute.Status = (int)DisputeStatus.UnderReview;
                dispute.UpdatedAt = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            return evidence;
        }

        public async Task<Mdispute> ResolveDispute(string disputeId, ResolveDisputeDto resolveDto)
        {
            var dispute = await _context.Mdisputes
                .Include(d => d.Order)
                .FirstOrDefaultAsync(d => d.DisputeId == disputeId);
            if (dispute == null)
                throw new Exception("Dispute not found");

            if (dispute.Status == (int)DisputeStatus.Resolved || dispute.Status == (int)DisputeStatus.Closed)
                throw new Exception("Dispute is already resolved or closed");

            dispute.Resolution = resolveDto.Resolution;
            dispute.RefundAmount = resolveDto.RefundAmount;
            dispute.AdminNote = resolveDto.AdminNote;
            dispute.ResolvedBy = resolveDto.ResolvedBy;
            dispute.Status = (int)DisputeStatus.Resolved;
            dispute.ResolvedAt = DateTime.Now;
            dispute.UpdatedAt = DateTime.Now;

            // Update order status based on resolution
            var resolution = (DisputeResolution)resolveDto.Resolution;
            var order = dispute.Order;

            if (resolution == DisputeResolution.FullRefund
                || resolution == DisputeResolution.PartialRefund
                || resolution == DisputeResolution.ReturnAndRefund)
            {
                order.OrderStatus = (int)OrderStatus.Refunded;
                order.UpdatedAt = DateTime.Now;
            }
            else if (resolution == DisputeResolution.Rejected
                || resolution == DisputeResolution.MutualAgreement)
            {
                order.OrderStatus = (int)OrderStatus.Completed;
                order.CompletedAt = DateTime.Now;
                order.UpdatedAt = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            return dispute;
        }

        public async Task<List<MdisputeEvidence>> GetEvidencesByDisputeIdAsync(string disputeId)
        {
            return await _context.MdisputeEvidences
                .Include(e => e.Submitter)
                .Where(e => e.DisputeId == disputeId)
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();
        }
    }
}
