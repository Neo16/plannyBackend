using PlannyBackend.Bll.BllModels;
using PlannyBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannyBackend.Interfaces
{
    public interface IPlannyService
    {
        Task CreatePlanny(PlannyProposal planny);
        Task<PlannyProposal> GetPlannyProposalById(int Id);      

        Task<List<PlannyProposal>> GetPlannyProposalsOfUser(int userId);

        Task JoinProposal(int proposalId, int userId);

        Task ApproveParticipation(int participationId, int currentUserId);
        Task DeclineParticipation(int participationId, int currentUserId);

        Task<List<PlannyProposal>> SearchPlannyProposals(ProposalQuery query);
        Task CancelParticipation(int proposalId, int currentUserId);
        Task DeleteProposa(int id);
        Task<List<Participation>> GetParticipationsForUser(int userId);
    }
}
