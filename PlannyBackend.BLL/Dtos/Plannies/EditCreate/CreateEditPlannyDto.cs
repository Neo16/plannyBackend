using PlannyBackend.Model;
using PlannyBackend.Model.Enums;
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
        public int OwnerId { get; set; }
        public string PictureUrl { get; set; }

        public List<int> CategoryIds { get; set; }       
        public int? MaxParticipants { get; set; }
        public int? MinRequeredAge { get; set; }
        public int? MaxRequeredAge { get; set; }       
        public Gender? RequiredGenger { get; set; }
    }   
}
