using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannyBackend.Web.Dtos.Account
{
    public class LoginResultDto
    {
        public string UserToken { get; set; }

        public string UserName { get; set; }
    }
}
