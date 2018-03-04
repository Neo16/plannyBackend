using AutoMapper;
using PlannyBackend.Bll.BllModels;
using PlannyBackend.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannyBackend.Common.Mappings
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
