using AutoMapper;
using PlannyBackend.Bll.BllModels;
using PlannyBackend.Web.Dtos;

namespace PlannyBackend.Web.Mappings
{
    public class EntityToDtoMappingProfile : Profile
    {
        public EntityToDtoMappingProfile()
        {
            CreateMap<ProposalQuery, ProposalQueryDto>()    
             .ReverseMap();
        }
    }
}
