using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.Interfaces.Repositories
{
    public interface IDisputeRepository
    {
        Task<Mdispute> CreateDispute(DisputeDto disputeDto);
        Task<Mdispute?> GetDisputeByIdAsync(string disputeId);
        Task<Mdispute?> GetDisputeByOrderIdAsync(string orderId);
        Task<List<Mdispute>> GetAllDisputesAsync();
        Task<MdisputeEvidence> AddEvidence(DisputeEvidenceDto evidenceDto);
        Task<Mdispute> ResolveDispute(string disputeId, ResolveDisputeDto resolveDto);
        Task<List<MdisputeEvidence>> GetEvidencesByDisputeIdAsync(string disputeId);
    }
}
