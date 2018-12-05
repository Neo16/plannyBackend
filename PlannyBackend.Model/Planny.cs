using PlannyBackend.Model.Enums;
using PlannyBackend.Model.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannyBackend.Model
{
    public class Planny
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

        public ICollection<PlannyCategory> PlannyCategorys { get; set; }    

        public string PictureUrl { get; set; }

        #region participation conditions
        
        public int? MaxParticipants { get; set; }
        public int? MinRequeredAge { get; set; }
        public int? MaxRequeredAge { get; set; }
        public Gender? Gender { get; set; }

#endregion
    }
}
