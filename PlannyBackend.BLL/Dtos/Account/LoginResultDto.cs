using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannyBackend.Bll.Dtos.Account
{
    public class LoginResultDto
    {
        public string UserToken { get; set; }

        public string UserName { get; set; }
    }
}
