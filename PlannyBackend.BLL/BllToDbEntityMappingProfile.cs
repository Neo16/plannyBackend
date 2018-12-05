
using AutoMapper;
using PlannyBackend.BLL.Dtos;
using PlannyBackend.BLL.Dtos.Profile;
using PlannyBackend.Model;
using PlannyBackend.Model.Identity;
using System;
using System.Linq;

namespace PlannyBackend.BLL
{
    public class BllToDbEntityMappingProfile : Profile
    {
        public BllToDbEntityMappingProfile()
        {
            CreateMap<RegisterDto, ApplicationUser>()
                .ForMember(e => e.UserName, f => f.MapFrom(k => k.UserName))
                .ForMember(e => e.Email, f => f.MapFrom(k => k.Email))
                .ForMember(e => e.BirthDate, f => f.MapFrom(k => k.BirthDate))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Planny, PlannyDto>()
                .ForMember(e => e.Id, f => f.MapFrom(k => k.Id))
                .ForMember(e => e.Name, f => f.MapFrom(k => k.Name))
                .ForMember(e => e.Description, f => f.MapFrom(k => k.Description))
                .ForMember(e => e.FromTime, f => f.MapFrom(k => k.FromTime))
                .ForMember(e => e.ToTime, f => f.MapFrom(k => k.ToTime))
                .ForMember(e => e.MaxParticipants, f => f.MapFrom(k => k.MaxParticipants))
                .ForMember(e => e.MinRequeredAge, f => f.MapFrom(k => k.MinRequeredAge))
                .ForMember(e => e.MaxRequeredAge, f => f.MapFrom(k => k.MaxRequeredAge))             
                .ForMember(e => e.PictureUrl, f => f.MapFrom(k => k.PictureUrl))
                .ForMember(e => e.Categories, f => f.MapFrom(k => k.PlannyCategorys))
                .ForMember(e => e.Location, f => f.MapFrom(k => k.Location))
                .ForMember(e => e.OwnerName, f => f.MapFrom(k => k.Owner.UserName))
                .ForMember(e => e.OwnerId, f => f.MapFrom(k => k.Owner.Id));

            CreateMap<CreateEditPlannyDto, Planny>()
                .ForMember(e => e.Name, f => f.MapFrom(k => k.Name))
                .ForMember(e => e.Description, f => f.MapFrom(k => k.Description))
                .ForMember(e => e.FromTime, f => f.MapFrom(k => k.FromTime))
                .ForMember(e => e.ToTime, f => f.MapFrom(k => k.ToTime))          
                .ForMember(e => e.MaxParticipants, f => f.MapFrom(k => k.MaxParticipants))
                .ForMember(e => e.MinRequeredAge, f => f.MapFrom(k => k.MinRequeredAge))
                .ForMember(e => e.MaxRequeredAge, f => f.MapFrom(k => k.MaxRequeredAge))              
                .ForMember(e => e.PictureUrl, f => f.MapFrom(k => k.PictureUrl))
                .ForMember(e => e.PlannyCategorys, f => f.MapFrom(k => k.CategoryIds))
                .ForMember(e => e.Location, f => f.MapFrom(k => k.Location));

            CreateMap<int, PlannyCategory>()
                .ForMember(e => e.CategoryId, f => f.MapFrom(k => k));

            CreateMap<PlannyCategory, CategoryDto>()              
              .ForMember(e => e.Name, f => f.MapFrom(k => k.Category.Name));

            CreateMap<Planny, PlannyDtoWithParticipations>()
                .IncludeBase<Planny, PlannyDto>()
                .ForMember(e => e.Participations, f => f.MapFrom(k => k.Participations));

            CreateMap<Participation, ParticipationDto>()
                .ForMember(e => e.UserName, f => f.MapFrom(k => k.User.UserName))
                .ForMember(e => e.UserId, f => f.MapFrom(k => k.User.Id))
                .ForMember(e => e.ParticipationId, f => f.MapFrom(k => k.Id))
                .ForMember(e => e.State, f => f.MapFrom(k => k.State));

            CreateMap<Location, LocationDto>()
                .ForMember(e => e.Address, f => f.MapFrom(k => k.Address))
                .ForMember(e => e.Id, f => f.MapFrom(k => k.Id))
                .ForMember(e => e.Lonlongitude, f => f.MapFrom(k => k.Longitude))
                .ForMember(e => e.Latitude, f => f.MapFrom(k => k.Latitude))
                .ReverseMap();

            CreateMap<Participation, MyParticipationDto>()
               .ForMember(e => e.Id, f => f.MapFrom(k => k.Id))
               .ForMember(e => e.State, f => f.MapFrom(k => k.State))
               .ForMember(e => e.PlannyId, f => f.MapFrom(k => k.Planny.Id))
               .ForMember(e => e.PlannyName, f => f.MapFrom(k => k.Planny.Name))
               .ForMember(e => e.PlannyDescription, f => f.MapFrom(k => k.Planny.Description))
               .ForMember(e => e.PlannyPictureUrl, f => f.MapFrom(k => k.Planny.PictureUrl))
               .ForMember(e => e.PlannyFromTime, f => f.MapFrom(k => k.Planny.FromTime))
               .ForMember(e => e.PlannyToTime, f => f.MapFrom(k => k.Planny.ToTime))
               .ForMember(e => e.OwnerName, f => f.MapFrom(k => k.Planny.Owner.UserName));

            CreateMap<ApplicationUser, ProfileDto>()
               .ForMember(e => e.Gender, f => f.MapFrom(k => k.Gender))
               .ForMember(e => e.UserName, f => f.MapFrom(k => k.UserName))
               .ForMember(e => e.Age, f => f.MapFrom(k => DateTime.Now.Year - k.BirthDate.Year))
               .ForMember(e => e.Gender, f => f.MapFrom(k => k.Gender))
               .ForMember(e => e.SelfIntroduction, f => f.MapFrom(k => k.SelfIntroduction))
               .ForMember(e => e.PictureUrl, f => f.MapFrom(k => k.PictureUrl));

            CreateMap<ApplicationUser, EditProfileDto>()
             .ForMember(e => e.Gender, f => f.MapFrom(k => k.Gender))
             .ForMember(e => e.UserName, f => f.MapFrom(k => k.UserName))
             .ForMember(e => e.BirthDate, f => f.MapFrom(k => k.BirthDate))
             .ForMember(e => e.Gender, f => f.MapFrom(k => k.Gender))
             .ForMember(e => e.SelfIntroduction, f => f.MapFrom(k => k.SelfIntroduction))
             .ForMember(e => e.PictureUrl, f => f.MapFrom(k => k.PictureUrl));
        }
    }
}