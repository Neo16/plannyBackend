using AutoMapper;
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
            var profileEnt = await _context.Users
                 .Where(e => e.Id == id)     
                 .SingleOrDefaultAsync();

            return Mapper.Map<ProfileDto>(profileEnt);

        }

        public async Task Edit(int id, EditProfileDto profile)
        {
            var user = await _context.Users
               .FirstOrDefaultAsync(e => e.Id == id);

            if (user != null)
            {
                user.UserName = profile.UserName;
                user.BirthDate = profile.BirthDate;
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
