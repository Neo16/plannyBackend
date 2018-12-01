using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Collections.Sequences;
using PlannyBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannyBackend.Web.Dtos
{
    public class PlannyProposalDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime FromTime { get; set; }

        public DateTime ToTime { get; set; }

        public LocationDto Location { get; set; }

        public int CategoryId { get; set; }

        public String[] CategoryNames { get; set; }

        public int MinParticipants { get; set; }
        public int MaxParticipants { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public bool IsOnlyForFriends { get; set; }
        public bool IsNearOwner { get; set; }
        public bool IsSimilarInterest { get; set; }
        public string PictureName { get; set; }
        public string ParticipationState  { get; set; }

        public PlannyProposalDto(PlannyProposal original)
        {            
            this.Id = original.Id;
            this.Name = original.Name;
            this.Description = original.Description;
            this.FromTime = original.FromTime;
            this.ToTime = original.ToTime;         
            this.CategoryId = original.CategoryId;

            this.MinParticipants = original.MinParticipants;
            this.MaxParticipants = original.MaxParticipants;
            this.MinAge = original.MinAge;
            this.MaxAge = original.MaxAge;
            this.IsOnlyForFriends = original.IsOnlyForFriends;
            this.IsNearOwner = original.IsNearOwner;
            this.IsSimilarInterest = original.IsSimilarInterest;
            this.PictureName = original.PictureUrl;
            this.CategoryNames = new string[3];
            this.CategoryNames[0] = original.Category.Name;

            if (original.Location != null)
            {
                this.Location = new LocationDto(original.Location);
            }
        }

        public PlannyProposal ToEntity()
        {
            var result = new PlannyProposal();
            result.Id = this.Id;
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

            if (this.Location != null)
            {
                result.Location = this.Location.ToEntity();
            }

            return result;
        }
    }   
}
