using PlannyBackend.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannyBackend.Web.Dtos
{
    public class ParticipantDto
    {
        public string UserName { get; set; }
        public int UserId { get; set; }
        public int ParticipationId { get; set; }
        public string State { get; set; }
    }
}
