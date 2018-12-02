using System.Collections.Generic;

namespace PlannyBackend.Bll.Dtos
{
    public class PlannyDtoWithParticipants : PlannyDto
    {
        public List<ParticipationDto> Participations { get; set; }      
    }
}
