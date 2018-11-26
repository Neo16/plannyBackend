using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlannyBackend.Models;

namespace PlannyBackend.Web.Dtos
{
    public class PlannyProposalDtoWithParticipants : PlannyProposalDto
    {
        public List<ParticipantDto> Participants { get; set; }
        public PlannyProposalDtoWithParticipants(PlannyProposal original) : base(original)
        {
            this.Participants = new List<ParticipantDto>();

            foreach (Participation p in  original.Participations)
            {
                this.Participants.Add(new ParticipantDto()
                {
                    ParticipationId = p.Id,
                    State = p.State.ToString(),
                    UserId = p.UserId.Value,
                    UserName = p.User.UserName
                });
            }
        }         
    }
}
