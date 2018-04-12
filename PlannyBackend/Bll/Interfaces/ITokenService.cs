using PlannyBackend.Dtos.Account;
using PlannyBackend.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannyBackend.Bll.Interfaces
{
    public interface ITokenService
    {
        Task<TokenDto> GetTokenForUserAsync(ApplicationUser user);
    }
}
