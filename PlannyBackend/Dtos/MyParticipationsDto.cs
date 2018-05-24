using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlannyBackend.Models;

namespace PlannyBackend.Dtos
{
    public class MyParticipationsDto
    {
        public int Id { get; set; }

        public string State { get; set; }

        public string PlannyName { get; set; }

        public MyParticipationsDto(Participation e)
        {
            this.Id = e.Id;
            this.State = e.State.ToString();
            this.PlannyName = e.PlannyProposal.Name;
        }
    }
}
