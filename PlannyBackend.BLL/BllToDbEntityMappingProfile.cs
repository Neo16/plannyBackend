
using AutoMapper;
using PlannyBackend.BLL.Dtos;
using PlannyBackend.Model;
using PlannyBackend.Model.Identity;
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
                .ForMember(e => e.Age, f => f.MapFrom(k => k.Age))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Planny, PlannyDto>()
                .ForMember(e => e.Id, f => f.MapFrom(k => k.Id))
                .ForMember(e => e.Name, f => f.MapFrom(k => k.Name))
                .ForMember(e => e.Description, f => f.MapFrom(k => k.Description))
                .ForMember(e => e.FromTime, f => f.MapFrom(k => k.FromTime))
                .ForMember(e => e.ToTime, f => f.MapFrom(k => k.ToTime))
                .ForMember(e => e.MinParticipants, f => f.MapFrom(k => k.MinParticipants))
                .ForMember(e => e.MaxParticipants, f => f.MapFrom(k => k.MaxParticipants))
                .ForMember(e => e.MinAge, f => f.MapFrom(k => k.MinAge))
                .ForMember(e => e.MaxAge, f => f.MapFrom(k => k.MaxAge))
                .ForMember(e => e.IsOnlyForFriends, f => f.MapFrom(k => k.IsOnlyForFriends))
                .ForMember(e => e.IsNearOwner, f => f.MapFrom(k => k.IsNearOwner))
                .ForMember(e => e.IsSimilarInterest, f => f.MapFrom(k => k.IsSimilarInterest))
                .ForMember(e => e.PictureUrl, f => f.MapFrom(k => k.PictureUrl))
                .ForMember(e => e.Categories, f => f.MapFrom(k => k.PlannyCategories))
                .ForMember(e => e.Location, f => f.MapFrom(k => k.Location));           

            CreateMap<CreateEditPlannyDto, Planny>()
                .ForMember(e => e.Name, f => f.MapFrom(k => k.Name))
                .ForMember(e => e.Description, f => f.MapFrom(k => k.Description))
                .ForMember(e => e.FromTime, f => f.MapFrom(k => k.FromTime))
                .ForMember(e => e.ToTime, f => f.MapFrom(k => k.ToTime))
                .ForMember(e => e.MinParticipants, f => f.MapFrom(k => k.MinParticipants))
                .ForMember(e => e.MaxParticipants, f => f.MapFrom(k => k.MaxParticipants))
                .ForMember(e => e.MinAge, f => f.MapFrom(k => k.MinAge))
                .ForMember(e => e.MaxAge, f => f.MapFrom(k => k.MaxAge))
                .ForMember(e => e.IsOnlyForFriends, f => f.MapFrom(k => k.IsOnlyForFriends))
                .ForMember(e => e.IsNearOwner, f => f.MapFrom(k => k.IsNearOwner))
                .ForMember(e => e.IsSimilarInterest, f => f.MapFrom(k => k.IsSimilarInterest))
                .ForMember(e => e.PictureUrl, f => f.MapFrom(k => k.PictureUrl))
                .ForMember(e => e.PlannyCategories, f => f.MapFrom(k => k.CategoryIds))
                .ForMember(e => e.Location, f => f.MapFrom(k => k.Location));

            CreateMap<int, PlannyCategory>()
                .ForMember(e => e.CategoryId, f => f.MapFrom(k => k));

            CreateMap<PlannyCategory, CategoryDto>()              
              .ForMember(e => e.Name, f => f.MapFrom(k => k.Category.Name));

            CreateMap<Planny, PlannyDtoWithParticipants>()
                .IncludeBase<Planny, PlannyDto>()
                .ForMember(e => e.Participations, f => f.MapFrom(k => k.Participations));

            CreateMap<Participation, ParticipationDto>()
                .ForMember(e => e.UserName, f => f.MapFrom(k => k.User.UserName))
                .ForMember(e => e.UserId, f => f.MapFrom(k => k.User.Id))
                .ForMember(e => e.ParticipationId, f => f.MapFrom(k => k.Id))
                .ForMember(e => e.State, f => f.MapFrom(k => k.State))
                .ForMember(e => e.PlannyName, f => f.MapFrom(k => k.Planny.Name));

            CreateMap<Location, LocationDto>()
                .ForMember(e => e.Address, f => f.MapFrom(k => k.Address))
                .ForMember(e => e.Id, f => f.MapFrom(k => k.Id))
                .ForMember(e => e.Lonlongitude, f => f.MapFrom(k => k.Longitude))
                .ForMember(e => e.Latitude, f => f.MapFrom(k => k.Latitude))
                .ReverseMap();
        }
    }
}