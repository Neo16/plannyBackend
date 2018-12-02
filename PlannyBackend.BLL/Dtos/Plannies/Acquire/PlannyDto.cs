using System;
using System.Collections.Generic;

namespace PlannyBackend.Bll.Dtos
{
    public class PlannyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime FromTime { get; set; }

        public DateTime ToTime { get; set; }

        public LocationDto Location { get; set; }

        public List<string> CategoryNames { get; set; }

        public int MinParticipants { get; set; }
        public int MaxParticipants { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public bool IsOnlyForFriends { get; set; }
        public bool IsNearOwner { get; set; }
        public bool IsSimilarInterest { get; set; }
        public string PictureUrl { get; set; }
        public string ParticipationState  { get; set; }
    }   
}
