using Microsoft.AspNetCore.Http;
using PlannyBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannyBackend.Dtos
{
    public class CreatePlannyProposalDto
    {       
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime FromTime { get; set; }

        public DateTime ToTime { get; set; }

        public LocationDto Location { get; set; }

        public int CategoryId { get; set; }

        public int MinParticipants { get; set; }
        public int MaxParticipants { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public bool IsOnlyForFriends { get; set; }
        public bool IsNearOwner { get; set; }
        public bool IsSimilarInterest { get; set; }
        public int OwnerId { get; set; }
        public string PictureName { get; set; }

        public PlannyProposal ToEntity()
        {
            var result = new PlannyProposal();           
            result.Name = this.Name;
            result.Description = this.Description;
            result.FromTime = this.FromTime;
            result.ToTime = this.ToTime;           
            result.CategoryId = this.CategoryId;
            result.MinParticipants = this.MinParticipants;
            result.MaxParticipants = this.MaxParticipants;
            result.MinAge = this.MinAge;
            result.MaxAge = this.MaxAge;
            result.IsOnlyForFriends = this.IsOnlyForFriends;
            result.IsNearOwner = this.IsNearOwner;
            result.IsSimilarInterest = this.IsSimilarInterest;
            result.OwnerId = this.OwnerId;
            result.PictureName = this.PictureName;

            if (this.Location != null)
            {
                result.Location = this.Location.ToEntity();
            }

            return result;
        }
    }   
}
