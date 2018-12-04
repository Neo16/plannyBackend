using System.Collections.Generic;

namespace PlannyBackend.BLL.Dtos
{
    public class PlannyDtoWithParticipations : PlannyDto
    {
        public List<ParticipationDto> Participations { get; set; }      
    }
}
