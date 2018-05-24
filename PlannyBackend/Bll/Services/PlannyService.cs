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
using Geolocation;

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
                PlannyProposal = proposal
            });

            await _context.SaveChangesAsync();
        }

        public async Task<List<PlannyProposal>> SearchPlannyProposals(ProposalQuery query)
        {
            var plannies = _context.PlannyProposals
                 .Include(e => e.Category)
                 .Include(e => e.Location)
                 .AsQueryable();

            var filtered = plannies;


            //kategoriára 
            if (query.CategoryIds.Count > 0)
            {
                filtered = filtered.Where(e => query.CategoryIds.Contains(e.CategoryId));
            }

            //TODO: szűrők kiíróra és résztvevőkre
            if (query.ParticipantsAgeMax != 0)
            {
                filtered = filtered.Where(e => e.MaxAge <= query.ParticipantsAgeMax);
            }

            if (query.ParticipantsAgeMin != 0)
            {
                filtered = filtered.Where(e => e.MinAge >= query.ParticipantsAgeMin);
            }

            //TODO: Szűrők Helyszínre
            if (query.Longitude != 0 && query.Latitude != 0 && query.RangeInKms != 0)
            {
                Coordinate c = new Coordinate()
                {
                    Latitude = query.Latitude,
                    Longitude = query.Longitude
                };

                filtered = filtered.Where(e => IsInRange(e.Location, c, query.RangeInKms));
            }

            //TODO: szűrők Dátumra
            if (query.FromTime != null && query.FromTime > (DateTime.Now.AddYears(-1)))
            {
                filtered = filtered.Where(e => e.FromTime >= query.FromTime);
            }

            if (query.ToTime != null && query.ToTime > DateTime.Now)
            {
                filtered = filtered.Where(e => e.ToTime <= query.ToTime);
            }

            //TODO order
            var ordered = filtered;

            return await ordered.ToListAsync();
        }

        private bool IsInRange(Location l, Coordinate c, double range)
        {
            if (l != null)
            {
                var c2 = new Coordinate()
                {
                    Longitude = l.Longitude,
                    Latitude = l.Latitude
                };

                var d = GeoCalculator.GetDistance(c, c2, 3);
                return d < range * 0.621371192;
            }

            return true;
        }



        public async Task ApproveParticipation(int participationId)
        {
            var currentUserId = (await _userService.GetCurrentUser()).Id;
            var participation = await _context.Participations
                .Include(e => e.PlannyProposal)
                .ThenInclude(e => e.Owner)
                .Where(e => e.Id == participationId)
                .SingleAsync();

            if (participation.PlannyProposal.OwnerId == currentUserId)
            {
                participation.State = ParticipationState.Approved;
                await _context.SaveChangesAsync();
            }
            else
            {
                //TODO hiba 
            }
        }

        public async Task DeclineParticipation(int participationId)
        {
            var currentUserId = (await _userService.GetCurrentUser()).Id;
            var participation = await _context.Participations
                .Include(e => e.PlannyProposal)
                .ThenInclude(e => e.Owner)
                .Where(e => e.Id == participationId)
                .SingleAsync();

            if (participation.PlannyProposal.OwnerId == currentUserId)
            {
                participation.State = ParticipationState.Required;
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
               .Include(e => e.Participations)
               .ThenInclude(p => p.User)
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

        public async Task DeleteProposa(int id)
        {
            var proposal = await _context.PlannyProposals
                .Where(e => e.Id == id)
                .SingleOrDefaultAsync();

            _context.PlannyProposals.Remove(proposal);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Participation>> GetMyParticipations()
        {
            var currentUserId = (await _userService.GetCurrentUser()).Id;

            return await _context.Participations             
                .Include(e => e.PlannyProposal)
                .Where(e => e.UserId == currentUserId)
                .ToListAsync();            
        }
    }
}
