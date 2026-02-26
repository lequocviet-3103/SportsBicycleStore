using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Extension;
using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Application.SearchFilter;
using SportsBicycleStore.Domain.Entities;
using SportsBicycleStore.Domain.Enum;
using SportsBicycleStore.Infastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Infastructure.Repositories
{
    public class UserRepository : Repository<Muser>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<Muser?> GetByEmailAsync(string email)
        {
            return await _context.Musers.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Muser?> GetByUserIdAsync(string userId)
        {
            return await _context.Musers.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<PagedResult<Muser>> GetUsersAsync(UserSearchFilter filter)
        {
            var query =  _context.Musers.AsQueryable();
            if (!string.IsNullOrEmpty(filter.UserId))
            {
                query =  query.Where(u => u.UserId.Contains(filter.UserId));
            }
            if (!string.IsNullOrEmpty(filter.UserName))
            {
                query =  query.Where(u => u.UserName.Contains(filter.UserName));
            }
            if (!string.IsNullOrEmpty(filter.Email))
            {
                query  =  query.Where(u => u.Email.Contains(filter.Email));
            }
            if (!string.IsNullOrEmpty(filter.PhoneNumber))
            {
                query = query.Where(u => u.PhoneNumber != null && u.PhoneNumber.Contains(filter.PhoneNumber));
            }
            if (!string.IsNullOrEmpty(filter.FullName))
            {
                query = query.Where(u => u.FullName != null && u.FullName.Contains(filter.FullName));
            }
            if (!string.IsNullOrEmpty(filter.Address))
            {
                query = query.Where(u => u.Address != null && u.Address.Contains(filter.Address));
            }
            //if (filter.DateOfBirth.HasValue)
            //{
            //    query = query.Where(u => u.DateOfBirth == filter.DateOfBirth);
            //}
            if (!string.IsNullOrEmpty(filter.RoleId))
            {
                query = query.Where(u => u.RoleId == filter.RoleId);
            }
            var totalCount = query.Count();

            var items = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new PagedResult<Muser>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };
        }

        public async Task<Muser?> RegisterUserAsync(RegisterUserDto dto)
        {
            var user = new Muser
            {
                UserId = Guid.NewGuid().ToString(),
                UserName = dto.UserName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.PasswordHash), // Use BCrypt.Net.BCrypt
                PhoneNumber = dto.PhoneNumber,
                FullName = dto.FullName,
                Gender = dto.Gender,
                Status = (int)UserStatus.Active,
                RoleId = ((int)EnumRole.Buyer).ToString(),
            };
            await _context.Musers.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        

        public async Task<Muser?> UpdateUserAsync(string userId, UpdateUserDto dto)
        {
            var user = await _context.Musers.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
            {
                return null; 
            }

            if (!string.Equals(user.Email, dto.Email, StringComparison.OrdinalIgnoreCase))
            {
                bool emailExists = await _context.Musers
                    .AnyAsync(u => u.Email == dto.Email && u.UserId != userId);

                if (emailExists)
                {
                    throw new SportsBicycleStore.Application.Exceptions.UserFriendlyException(
                        statusCode: 400,
                        errorCode: "EMAIL_ALREADY_EXISTS",
                        message: "Email is already in use by another user.");
                }
            }

            if (!string.Equals(user.UserName, dto.UserName, StringComparison.OrdinalIgnoreCase))
            {
                bool userNameExists = await _context.Musers
                    .AnyAsync(u => u.UserName == dto.UserName && u.UserId != userId);

                if (userNameExists)
                {
                    throw new SportsBicycleStore.Application.Exceptions.UserFriendlyException(
                        statusCode: 400,
                        errorCode: "USERNAME_ALREADY_EXISTS",
                        message: "Username is already in use by another user.");
                }
            }

            user.UserName = dto.UserName;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            user.FullName = dto.FullName;
            user.AvatarUrl = dto.AvatarUrl;
            user.Address = dto.Address;
            user.DateOfBirth = dto.DateOfBirth;
            user.Gender = dto.Gender;
            user.Status = dto.Status;
            user.RoleId = dto.RoleId;
            user.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return user;
        }
        public async Task<bool> SoftDeleteUserAsync(string userId, UserStatus status)
        {
            var user = await _context.Musers.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
            {
                return false;
            }

            user.Status = (int)status;
            user.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<Muser?> UpdateInfoUserDto(string userId, UpdateInfoUserDto updateInfoUserDto)
        {
            var user = await _context.Musers.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
            {
                return null;
            }
            user.PhoneNumber = updateInfoUserDto.PhoneNumber;
            user.FullName = updateInfoUserDto.FullName;
            user.AvatarUrl = updateInfoUserDto.AvatarUrl;
            user.Address = updateInfoUserDto.Address;
            user.DateOfBirth = updateInfoUserDto.DateOfBirth;
            user.Gender = updateInfoUserDto.Gender;
            user.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return user;

        }


        public async Task<Muser?> ForgetPasswordDto(string email, ForgetPasswordDto forgetPasswordDto)
        {
            var user = await _context.Musers.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return null;
            }
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(forgetPasswordDto.PasswordHash);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
