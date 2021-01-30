using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Identity.Processor.Services.MappingProfiles;

namespace Nano35.Identity.Processor.Services.AppStart.Configure
{
    public class AutoMapperConfiguration : 
        IConfigurationOfService
    {
        public void AddToServices(
            IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
            IMapper mapper = mapperConfig.CreateMapper();
            MappingPipe.Mapper = mapper;
            services.AddSingleton(mapper);
        }
    }
}