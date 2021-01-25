using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Nano35.Identity.Api.Services;
using Nano35.Identity.Api.Services.Middlewares;

namespace Nano35.Identity.Api.Services.Constructors
{
    public static partial class ConfigureEndpoints
    {
        public static void Configure(IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("please use swagger");
                });
            });
        }
    }
}