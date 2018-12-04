using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PlannyBackend.BLL.Dtos.Profile;
using PlannyBackend.BLL.Exceptions;
using PlannyBackend.BLL.Interfaces;
using PlannyBackend.DAL;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PlannyBackend.BLL.Services
{
    public class ProfileService : IProfileService
    {
        private readonly ApplicationDbContext _context;

        public ProfileService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<ProfileDto> Get(int id)
        {
            return await _context.Users
                 .Where(e => e.Id == id)                 
                 .ProjectTo<ProfileDto>()
                 .SingleOrDefaultAsync();
        }

        public async Task Edit(int id, ProfileDto profile)
        {
            var user = await _context.Users
               .FirstOrDefaultAsync(e => e.Id == id);

            if (user != null)
            {
                user.UserName = profile.UserName;
                user.Age = profile.Age;
                user.Gender = profile.Gender;
                user.SelfIntroduction = profile.SelfIntroduction;
                user.PictureUrl = profile.PictureUrl;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new BusinessLogicException("User does not exist") { ErrorCode = ErrorCode.InvalidArgument };
            }
        }
    }
}
