using PlannyBackend.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannyBackend.Dtos.Account
{
    public class RegisterDto
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int Age { get; set; }

        public ApplicationUser toApplicationUser()
        {
            var user = new ApplicationUser()
            {
                UserName = this.UserName,
                Email = this.Email,
                Age = this.Age
            };
            return user;
        }
    }
}
