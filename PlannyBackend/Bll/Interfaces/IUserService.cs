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
        ApplicationUser GetCurrentUser();

    }
}
