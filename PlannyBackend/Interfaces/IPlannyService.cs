﻿using PlannyBackend.Models;
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

        Task<List<PlannyProposal>> GetPlannyMyProposals();
    }
}
