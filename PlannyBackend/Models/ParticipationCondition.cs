using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannyBackend.Models
{
    public class ParticipationCondition
    {
        public int MinParticipants { get; set; }
        public int MaxParticipants { get; set; }

        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public bool IsOnlyForFriends { get; set; }
        public bool IsNearOwner { get; set; }
        public bool IsSimilarInterest { get; set; }
    }
}
