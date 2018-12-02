using System.Collections.Generic;

namespace PlannyBackend.BLL.Dtos
{
    public class PlannyDtoWithParticipants : PlannyDto
    {
        public List<ParticipationDto> Participations { get; set; }      
    }
}
