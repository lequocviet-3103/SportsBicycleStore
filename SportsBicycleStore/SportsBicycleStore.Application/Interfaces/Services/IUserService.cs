using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Extension;
using SportsBicycleStore.Application.SearchFilter;
using SportsBicycleStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.Interfaces.Services
{
    public interface IUserService
    {
         Task<PagedResult<Muser>> GetUsersAsync(UserSearchFilter filter);
        Task<Muser?> UpdateUserAsync(string userId, UpdateUserDto dto);
        Task<bool> SoftDeleteUserAsync(string userId, SoftDeleteUserDto dto);
        Task<Muser?> UpdateInfoUserDto(string userId, UpdateInfoUserDto updateInfoUserDto);
        Task<bool> ForgetPasswordDto(string email, ForgetPasswordDto forgetPasswordDto);
    }
}
