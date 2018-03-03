using Microsoft.EntityFrameworkCore;
using PlannyBackend.Data;
using PlannyBackend.Interfaces;
using PlannyBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                .Where( e => e.OwnerId == currentUserId)
                .Include(e => e.Location)
                .ToListAsync();
        }

        public async Task<PlannyProposal> GetPlannyProposalById(int Id)
        {
            return await _context.PlannyProposals
                .Include(e => e.Location)
                .Where(e => e.Id == Id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<PlannyProposal>> GetPlannyProposals()
        {
            return await _context.PlannyProposals
                .Include(e => e.Location)
                .ToListAsync();
        }
    }
}
