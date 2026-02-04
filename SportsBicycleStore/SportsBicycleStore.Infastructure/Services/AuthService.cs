using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using BCrypt.Net;
using SportsBicycleStore.Domain.Entities; // Add this using directive for BCrypt


namespace SportsBicycleStore.Infastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator jwtTokenGenerator;
        public AuthService(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
        {
            this.jwtTokenGenerator = jwtTokenGenerator;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto request)
        {
            var user = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                return null;
            }

            bool isValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash); // Use BCrypt.Net.BCrypt
            if(!isValid)
            {
                return null;
            }
            var token = jwtTokenGenerator.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                Role = user.RoleId
            };

        }

        public async Task<bool> RegisterAsync(RegisterUserDto request)
        {
            var existingEmail = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
            //var existingUserId = _unitOfWork.UserRepository.GetByUserIdAsync(request.UserId);
            if (existingEmail != null)
            {
                return false;
            }


            await _unitOfWork.UserRepository.RegisterUserAsync(request);
            return true;


        }
    }
}
