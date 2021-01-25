using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Identity.Consumers.Models;
using Nano35.Identity.Consumers.Services.Contexts;

namespace Nano35.Identity.Consumers
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