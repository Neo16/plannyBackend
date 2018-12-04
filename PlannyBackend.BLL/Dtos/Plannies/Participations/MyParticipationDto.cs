using System;
using System.Collections.Generic;
using System.Text;

namespace PlannyBackend.BLL.Dtos
{
    public class MyParticipationDto
    {
        public int Id { get; set; }

        public string State { get; set; }

        public int PlannyId { get; set; } 

        public string PlannyName { get; set; }

        public string PlannyDescription { get; set; }

        public string PlannyPictureUrl { get; set; }

        public DateTime PlannyFromTime { get; set; }

        public DateTime PlannyToTime { get; set; }

        public string OwnerName { get; set; }
    }
}
