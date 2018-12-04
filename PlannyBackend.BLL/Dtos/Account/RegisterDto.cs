using PlannyBackend.Model.Identity;
using System;

namespace PlannyBackend.BLL.Dtos
{
    public class RegisterDto
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
