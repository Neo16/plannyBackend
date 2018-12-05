using System;
using System.Collections.Generic;
using System.Text;

namespace PlannyBackend.BLL.Dtos.Plannies.Acquire
{
    public class PlannyDtoWithJoinStatus : PlannyDto
    {
        public JoinStatus JoinStatus { get; set; }
    }
}
