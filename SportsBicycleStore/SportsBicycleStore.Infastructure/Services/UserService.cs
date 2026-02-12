using SportsBicycleStore.Application.DTO;
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
    public class UserService : IUserService
    {
        public readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<Muser>> GetUsersAsync(UserSearchFilter filter)
        {
            return await _unitOfWork.UserRepository.GetUsersAsync(filter);
        }

        

        public async Task<Muser?> UpdateUserAsync(string userId, UpdateUserDto dto)
        {
            return await _unitOfWork.UserRepository.UpdateUserAsync(userId, dto);
        }
        public async Task<bool> SoftDeleteUserAsync(string userId, SoftDeleteUserDto dto)
        {
            if (dto.Status < 1 || dto.Status > 3)
            {
                throw new SportsBicycleStore.Application.Exceptions.UserFriendlyException(
                    400,
                    "INVALID_STATUS",
                    "Status must be 1 (Active), 2 (Blocked) or 3 (Inactive)."
                );
            }
            var statusEnum = (UserStatus)dto.Status;
            return await _unitOfWork.UserRepository.SoftDeleteUserAsync(userId, statusEnum);
        }
    }
}
