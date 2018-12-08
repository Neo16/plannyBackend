
using PlannyBackend.Model.Enums;

namespace PlannyBackend.BLL.Dtos
{
    public class ParticipationDto
    {
        public string UserName { get; set; }
        public int UserId { get; set; }
        public int ParticipationId { get; set; }
        public ParticipationState State { get; set; }
    }
}
