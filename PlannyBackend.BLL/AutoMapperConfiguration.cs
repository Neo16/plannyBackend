using AutoMapper;

namespace PlannyBackend.BLL
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x => x.AddProfiles(typeof(AutoMapperConfiguration).Assembly));
        }
    }
}
