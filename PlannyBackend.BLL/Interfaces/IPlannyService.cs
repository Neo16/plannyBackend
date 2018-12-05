
using PlannyBackend.BLL.Dtos;
using PlannyBackend.BLL.Dtos.Plannies.Acquire;
using PlannyBackend.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlannyBackend.Interfaces
{
    public interface IPlannyService
    {
        Task CreatePlanny(CreateEditPlannyDto planny);

        Task UpdatePlanny(int id, CreateEditPlannyDto planny, int userId);

        Task<PlannyDtoWithJoinStatus> GetByIdWithJoinStatus(int Id, int currentUserId);      

        Task<List<PlannyDtoWithParticipations>> GetPlanniesOfUser(int userId);

        Task Join(int proposalId, int userId);

        Task ApproveParticipation(int participationId, int currentUserId);

        Task DeclineParticipation(int participationId, int currentUserId);

        Task<List<PlannyDto>> SearchPlannies(PlannyQueryDto query);

        Task CancelParticipation(int proposalId, int currentUserId);

        Task Delete(int id);

        Task<List<MyParticipationDto>> GetParticipationsOfUser(int userId);
    }
}
