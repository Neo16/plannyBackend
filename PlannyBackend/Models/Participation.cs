using PlannyBackend.Models.Enums;
using PlannyBackend.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannyBackend.Models
{
    public class Participation
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public int UserId { get; set; }
        public ParticipationState State { get; set; }


    }
}
