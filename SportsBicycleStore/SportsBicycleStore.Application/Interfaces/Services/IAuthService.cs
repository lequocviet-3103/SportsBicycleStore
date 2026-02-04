using SportsBicycleStore.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginRequestDto request);

        Task<bool> RegisterAsync(RegisterUserDto request);
    }
}
