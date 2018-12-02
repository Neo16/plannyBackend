using PlannyBackend.Model.Identity;
using System;
using System.Threading.Tasks;

namespace PlannyBackend.Interfaces
{
    public interface IUserService
    {       
        Task<(bool, string)> RegisterUser(ApplicationUser user, string password);
    }
}
