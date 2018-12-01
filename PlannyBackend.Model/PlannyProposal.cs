using PlannyBackend.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannyBackend.Models
{
    public class PlannyProposal
    {
        public int Id { get; set; }

        public ApplicationUser Owner { get; set; }
        public int OwnerId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime FromTime { get; set; }

        public DateTime ToTime { get; set; }

        public Location Location { get; set; }

        public List<Participation> Participations { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }

        public string PictureUrl { get; set; }

        #region participation conditions

        public int MinParticipants { get; set; }
        public int MaxParticipants { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public bool IsOnlyForFriends { get; set; }
        public bool IsNearOwner { get; set; }
        public bool IsSimilarInterest { get; set; }      

#endregion
    }
}
