using PlannyBackend.Model;
using System;
using System.Collections.Generic;

namespace PlannyBackend.BLL.Dtos
{
    public class CreateEditPlannyDto
    {       
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime FromTime { get; set; }

        public DateTime ToTime { get; set; }

        public LocationDto Location { get; set; }

        public List<int> CategoryIds { get; set; }

        public int MinParticipants { get; set; }
        public int MaxParticipants { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public bool IsOnlyForFriends { get; set; }
        public bool IsNearOwner { get; set; }
        public bool IsSimilarInterest { get; set; }
        public int OwnerId { get; set; }
        public string PictureUrl { get; set; }   
    }   
}
