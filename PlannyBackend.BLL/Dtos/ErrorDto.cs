using PlannyBackend.BLL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannyBackend.BLL.Dtos
{
    public class ErrorDto
    {
        public ErrorCode ErrorCode { get; set; }

        public string[] ErrorMessages { get; set; } = new string[0];
    }
}
