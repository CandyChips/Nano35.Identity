using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.Services.AppStart.Configure
{
    public class EntityFrameworkConfiguration : 
        IConfigurationOfService
    {
        public void AddToServices(
            IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options => 
                options.UseSqlServer("server=192.168.100.120; Initial Catalog=Nano35.Identity.DB; User id=sa; Password=Cerber666;"));
            services.AddScoped<IDataAccelerator, EntityFrameworkAccelerator>();
        }
    }
}