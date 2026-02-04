using SportsBicycleStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.Interfaces.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Muser user);
    }
}
