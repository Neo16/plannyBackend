using PlannyBackend.Dtos.Account;
using PlannyBackend.Models;
using PlannyBackend.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannyBackend.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetCurrentUser();
        Task<bool> RegisterUser(ApplicationUser user, string password);
    }
}
