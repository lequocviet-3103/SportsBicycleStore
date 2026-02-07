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
    }
}
