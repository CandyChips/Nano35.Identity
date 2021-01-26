using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.Services.Configure
{
    public static class IdentityServiceConstructor 
    {
        public static void Construct(
            IServiceCollection services) 
        {
            services.AddIdentity<User, Role>(opts =>
                {
                    opts.Password.RequiredLength = 6;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireUppercase = false;
                    opts.Password.RequireDigit = false;
                    opts.SignIn.RequireConfirmedEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();
        }
    }
}