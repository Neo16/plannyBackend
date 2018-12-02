﻿using PlannyBackend.Model.Identity;

namespace PlannyBackend.Bll.Dtos.Account
{
    public class RegisterDto
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int Age { get; set; }
    }
}