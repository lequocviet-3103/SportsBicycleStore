using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<Muser?> GetByEmailAsync(string email);

        Task<Muser?> GetByUserIdAsync(string userId);

        Task<Muser?> RegisterUserAsync(RegisterUserDto dto);

    }
}
