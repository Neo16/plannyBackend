using PlannyBackend.Model.Enums;
using PlannyBackend.Model.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannyBackend.Model
{
    public class Participation
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public int? UserId { get; set; }
        public ParticipationState State { get; set; }
        public int PlannyProposalId { get; set; }
        public Planny Planny { get; set; }
    }
}
