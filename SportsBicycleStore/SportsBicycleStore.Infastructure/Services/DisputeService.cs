using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Application.Interfaces.Services;
using SportsBicycleStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Infastructure.Services
{
    public class DisputeService : IDisputeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DisputeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Mdispute> CreateDispute(DisputeDto disputeDto)
        {
            return await _unitOfWork.DisputeRepository.CreateDispute(disputeDto);
        }

        public async Task<Mdispute?> GetDisputeByIdAsync(string disputeId)
        {
            return await _unitOfWork.DisputeRepository.GetDisputeByIdAsync(disputeId);
        }

        public async Task<Mdispute?> GetDisputeByOrderIdAsync(string orderId)
        {
            return await _unitOfWork.DisputeRepository.GetDisputeByOrderIdAsync(orderId);
        }

        public async Task<List<Mdispute>> GetAllDisputesAsync()
        {
            return await _unitOfWork.DisputeRepository.GetAllDisputesAsync();
        }

        public async Task<MdisputeEvidence> AddEvidence(DisputeEvidenceDto evidenceDto)
        {
            return await _unitOfWork.DisputeRepository.AddEvidence(evidenceDto);
        }

        public async Task<Mdispute> ResolveDispute(string disputeId, ResolveDisputeDto resolveDto)
        {
            return await _unitOfWork.DisputeRepository.ResolveDispute(disputeId, resolveDto);
        }

        public async Task<List<MdisputeEvidence>> GetEvidencesByDisputeIdAsync(string disputeId)
        {
            return await _unitOfWork.DisputeRepository.GetEvidencesByDisputeIdAsync(disputeId);
        }
    }
}
