﻿using PlannyBackend.Bll.BllModels;
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
        Task<List<PlannyProposal>> GetPlannyProposals();

        Task<List<PlannyProposal>> GetPlannyProposalsOfUser(int userId);

        Task<List<PlannyProposal>> GetPlannyMyProposals();
        Task JoinProposal(int proposalId);

        Task ApproveParticipation(int participationId);
        Task DeclineParticipation(int participationId);

        Task<List<PlannyProposal>> SearchPlannyProposals(ProposalQuery query);
        Task CancelParticipation(int proposalId);
        Task DeleteProposa(int id);
        Task<List<Participation>> GetMyParticipations();
    }
}
