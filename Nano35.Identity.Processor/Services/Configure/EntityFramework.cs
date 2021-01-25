
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Identity.Consumers.Services.Contexts;

namespace Nano35.Identity.Consumers
{
    public static class EntityFrameworkServiceConstructor 
    {
        public static void Construct(IServiceCollection services) 
        {
            services.AddDbContext<ApplicationContext>(options => 
                options.UseSqlServer("server=192.168.100.120; Initial Catalog=Nano35.Identity.DB; User id=sa; Password=Cerber666;"));
            services.AddScoped<IDataAccelerator, EntityFrameworkAccelerator>();
        }
    }
}