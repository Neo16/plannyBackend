using Microsoft.EntityFrameworkCore;
using PlannyBackend.Data;
using PlannyBackend.Interfaces;
using PlannyBackend.Models;
using PlannyBackend.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlannyBackend.Bll.BllModels;

namespace PlannyBackend.Services
{
    public class PlannyService : IPlannyService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public PlannyService(
            ApplicationDbContext contex,
            IUserService userService)
        {
            _context = contex;
            _userService = userService;

        }

        public async Task CreatePlanny(PlannyProposal planny)
        {
            _context.PlannyProposals.Add(planny);
            await _context.SaveChangesAsync();
        }

        public async Task<List<PlannyProposal>> GetPlannyMyProposals()
        {
            var currentUserId = _userService.GetCurrentUser().Id;

            return await _context.PlannyProposals
                .Where(e => e.OwnerId == currentUserId)
                .Include(e => e.Category)
                .Include(e => e.Location)
                .ToListAsync();
        }

        public async Task<PlannyProposal> GetPlannyProposalById(int Id)
        {
            return await _context.PlannyProposals
                .Include(e => e.Location)
                .Include(e => e.Participations)
                .Include(e => e.Category)
                .Where(e => e.Id == Id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<PlannyProposal>> GetPlannyProposals()
        {
            return await _context.PlannyProposals
                .Include(e => e.Category)
                .Include(e => e.Location)
                .ToListAsync();
        }

        public async Task JoinProposal(int id)
        {
            var currentUser = await _userService.GetCurrentUser();

            var proposal = await _context.PlannyProposals
                .Where(e => e.Id == id)
                .Include(e => e.Participations)
                .FirstAsync();

            proposal.Participations.Add(new Participation()
            {
                State = ParticipationState.Required,
                User = currentUser,
            });

            await _context.SaveChangesAsync();
        }

        public async Task<List<PlannyProposal>> SearchPlannyProposals(ProposalQuery query)
        {
            var plannies = _context.PlannyProposals.AsQueryable();

            //kategoriára 
            var filtered = plannies
                .Where(e => query.CategoryIds.Contains(e.CategoryId));

            //TODO: szűrők kiíróra és résztvevőkre

            //TODO: Szűrők Helyszínre

            //TODO: szűrők Dátumra

            //TODO order
            var ordered = filtered;

            return await ordered.ToListAsync();
        }

        public async Task ApproveParticipation(int proposalId, int participationId)
        {
            var proposal = await GetPlannyProposalById(proposalId);
            var currentUserId = _userService.GetCurrentUser().Id;

            if (proposal.OwnerId == currentUserId)
            {
                var participation = await _context
                   .Participations
                   .Where(e => e.Id == participationId)
                   .SingleAsync();

                participation.State = ParticipationState.Approved;

                await _context.SaveChangesAsync();

            }
            else
            {
                //TODO hiba 
            }
        }

        public async Task<List<PlannyProposal>> GetPlannyProposalsOfUser(int userId)
        {
            return await _context.PlannyProposals
               .Include(e => e.Location)
               .Include(e => e.Category)
               .Where(e => e.Owner.Id == userId)
               .ToListAsync();
        }

        public async Task CancelParticipation(int proposalId)
        {
            var currentUser = await _userService.GetCurrentUser();

            var proposal = await _context.PlannyProposals
                .Where(e => e.Id == proposalId)
                .Include(e => e.Participations)
                .FirstOrDefaultAsync();

            if (proposal != null)
            {
                var participation = proposal.Participations
               .Where(e => e.UserId == currentUser.Id)
               .First();

                proposal.Participations.Remove(participation);

                await _context.SaveChangesAsync();
            }
        }
    }
}
