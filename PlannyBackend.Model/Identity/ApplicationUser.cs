using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PlannyBackend.Model.Enums;

namespace PlannyBackend.Model.Identity
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<int>
    {
        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        public string SelfIntroduction { get; set; }

        public string PictureUrl { get; set; }

        public List<Participation> Participations { get; set; }          
        
    }
}
