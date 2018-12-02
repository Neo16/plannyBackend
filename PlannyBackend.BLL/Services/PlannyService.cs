using Microsoft.EntityFrameworkCore;
using PlannyBackend.Interfaces;
using PlannyBackend.Model;
using PlannyBackend.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geolocation;
using PlannyBackend.DAL;
using PlannyBackend.Bll.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using PlannyBackend.BLL.Exceptions;

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

        public async Task CreatePlanny(CreateEditPlannyDto planny)
        {
            Planny plannyEnt = Mapper.Map<Planny>(planny);
            _context.Plannies.Add(plannyEnt);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePlanny(int id, CreateEditPlannyDto planny, int userId)
        {
            Planny oldPlannyEnt = await _context.Plannies
                .Where(e => e.Owner.Id == userId)
                .Where(e => e.Id == id)
                .SingleOrDefaultAsync();

            if (oldPlannyEnt == null)
            {
                throw new BusinessLogicException("Invalid update request.") { ErrorCode = ErrorCode.InvalidArgument };
            }
            Planny newplannyEnt = Mapper.Map<Planny>(planny);

            newplannyEnt.Id = oldPlannyEnt.Id;
            foreach (var plannyCategory in newplannyEnt.PlannyCategories)
            {
                plannyCategory.Category = null;
                plannyCategory.PlannyId = newplannyEnt.Id;
            }
            if (oldPlannyEnt.PlannyCategories != null && oldPlannyEnt.PlannyCategories.Count() > 0)
            {
                _context.PlannyCategories.RemoveRange(oldPlannyEnt.PlannyCategories);
            }

            _context.PlannyCategories.AddRange(newplannyEnt.PlannyCategories);
            _context.Entry(oldPlannyEnt).CurrentValues.SetValues(newplannyEnt);
            await _context.SaveChangesAsync();      
        }

        public async Task<List<PlannyDtoWithParticipants>> GetPlanniesOfUser(int userId)
        {
            return await _context.Plannies
               .Where(e => e.Owner.Id == userId)
               .ProjectTo<PlannyDtoWithParticipants>()
               .ToListAsync();
        }

        public async Task<PlannyDtoWithParticipants> GetByIdWithParticipants(int Id)
        {
            return await _context.Plannies
               .Where(e => e.Id == Id)
               .ProjectTo<PlannyDtoWithParticipants>()
               .FirstOrDefaultAsync();
        }
    

        public async Task Join(int id, int currentUserId)
        {          
            var proposal = await _context.Plannies
                .Where(e => e.Id == id)
                .Include(e => e.Participations)
                .FirstAsync();

            proposal.Participations.Add(new Participation()
            {
                State = ParticipationState.Required,
                UserId = currentUserId,
                Planny = proposal
            });

            await _context.SaveChangesAsync();
        }

        public async Task<List<PlannyDto>> SearchPlannies(PlannyQueryDto query)
        {        
            var plannies = _context.Plannies               
                .AsQueryable();

            if (query == null)
            {
                plannies = plannies
                   //Todo legyen hozzáadás dátuma a plannyn 
                   .OrderBy(e => e.FromTime)
                   .Take(30);
            }
            else
            {
                //kategoriára 
                if (query.CategoryIds != null && query.CategoryIds.Count > 0)
                {
                    var filteredCategoryIds = query.CategoryIds;
                    plannies = plannies.Where(e => e.PlannyCategories.Any( c => filteredCategoryIds.Contains(c.CategoryId)));
                }

                //szűrők kiíróra és résztvevőkre
                if (query.ParticipantsAgeMax != 0)
                {
                    plannies = plannies.Where(e => e.MaxAge <= query.ParticipantsAgeMax);
                }

                if (query.ParticipantsAgeMin != 0)
                {
                    plannies = plannies.Where(e => e.MinAge >= query.ParticipantsAgeMin);
                }

                //Szűrők Helyszínre
                if (query.Longitude != 0 && query.Latitude != 0 && query.RangeInKms != 0)
                {
                    Coordinate c = new Coordinate()
                    {
                        Latitude = query.Latitude,
                        Longitude = query.Longitude
                    };

                    plannies = plannies.Where(e => IsInRange(e.Location, c, query.RangeInKms));
                }

                //szűrők Dátumra
                if (query.FromTime != null && query.FromTime > (DateTime.Now.AddYears(-1)))
                {
                    plannies = plannies.Where(e => e.FromTime >= query.FromTime);
                }

                if (query.ToTime != null && query.ToTime > DateTime.Now)
                {
                    plannies = plannies.Where(e => e.ToTime <= query.ToTime);
                }
            }                 
             
            //TODO rendezés és lapozás          

            return await plannies
                .ProjectTo<PlannyDto>()
                .ToListAsync();
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



        public async Task ApproveParticipation(int participationId, int currentUserId)
        {
            var participation = await _context.Participations
                .Include(e => e.Planny)
                .ThenInclude(e => e.Owner)
                .Where(e => e.Id == participationId)
                .SingleAsync();

            if (participation.Planny.OwnerId == currentUserId)
            {
                participation.State = ParticipationState.Approved;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new BusinessLogicException("Participation does not exist") { ErrorCode = ErrorCode.InvalidArgument };
            }
        }

        public async Task DeclineParticipation(int participationId, int currentUserId)
        {     
            var participation = await _context.Participations
                .Include(e => e.Planny)
                .ThenInclude(e => e.Owner)
                .Where(e => e.Id == participationId)
                .SingleAsync();

            if (participation.Planny.OwnerId == currentUserId)
            {
                participation.State = ParticipationState.Required;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new BusinessLogicException("Participation does not exist") { ErrorCode = ErrorCode.InvalidArgument };
            }
        }

        public async Task CancelParticipation(int proposalId, int currentUserId)
        {           
            var proposal = await _context.Plannies
                .Where(e => e.Id == proposalId)
                .Include(e => e.Participations)
                .FirstOrDefaultAsync();

            if (proposal != null)
            {
                var participation = proposal.Participations
               .Where(e => e.UserId == currentUserId)
               .First();

                proposal.Participations.Remove(participation);

                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var proposal = await _context.Plannies
                .Where(e => e.Id == id)
                .SingleOrDefaultAsync();

            _context.Plannies.Remove(proposal);

            await _context.SaveChangesAsync();
        }

        public async Task<List<ParticipationDto>> GetParticipationsForUser(int userId)
        {         
            return await _context.Participations  
                .Where(e => e.UserId == userId)
                .ProjectTo<ParticipationDto>()
                .ToListAsync();            
        }       
    }
}
