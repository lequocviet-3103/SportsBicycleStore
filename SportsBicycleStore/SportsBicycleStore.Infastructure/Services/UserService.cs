using SportsBicycleStore.Application.Extension;
using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Application.Interfaces.Services;
using SportsBicycleStore.Application.SearchFilter;
using SportsBicycleStore.Domain.Entities;
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
    }
}
