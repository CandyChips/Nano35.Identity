using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.MappingProfiles;

namespace Nano35.Identity.Processor.Configurations
{
    public class AutoMapperConfiguration : 
        IConfigurationOfService
    {
        public void AddToServices(
            IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UsersAutoMapperProfile());
                mc.AddProfile(new RolesAutoMapperProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            MappingPipe.Mapper = mapper;
            services.AddSingleton(mapper);
        }
    }
}