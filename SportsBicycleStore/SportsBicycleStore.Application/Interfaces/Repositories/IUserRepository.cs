using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Extension;
using SportsBicycleStore.Application.SearchFilter;
using SportsBicycleStore.Domain.Entities;
using SportsBicycleStore.Domain.Enum;
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
        Task<PagedResult<Muser>> GetUsersAsync(UserSearchFilter filter);
        Task<Muser?> UpdateUserAsync(string userId, UpdateUserDto dto);
        Task<bool> SoftDeleteUserAsync(string userId, UserStatus status);
        Task<Muser?> UpdateInfoUserDto(string userId, UpdateInfoUserDto updateInfoUserDto);
        Task<Muser?> ForgetPasswordDto(string email, ForgetPasswordDto forgetPasswordDto);
    }
}
