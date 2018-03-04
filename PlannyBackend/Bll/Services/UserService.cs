using PlannyBackend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlannyBackend.Models;
using PlannyBackend.Models.Identity;
using PlannyBackend.Data;

namespace PlannyBackend.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext contex)
        {
            _context = contex;
        }

        //egyelőre fake, amíg nincs rendes bejelentkezés, addig az első user a current 
        public ApplicationUser GetCurrentUser()
        {
            var user = _context.Users.FirstOrDefault();
            return user;
        }
    }
}
