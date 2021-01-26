using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Identity.Processor.Services.MappingProfiles;

namespace Nano35.Identity.Processor.Services.Configure
{
    public static class AutoMapperServiceConstructor 
    {
        public static void Construct(
            IServiceCollection services) 
        {
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
            IMapper mapper = mapperConfig.CreateMapper();
            MappingPipe.Mapper = mapper;
            services.AddSingleton(mapper);
        }
    }
}