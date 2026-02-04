using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Domain.Entities;
using SportsBicycleStore.Infastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SportsBicycleStore.Application.DTO;

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
                Gender = 1,
                Status = 1,
                RoleId = "3"
            };
            await _context.Musers.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
