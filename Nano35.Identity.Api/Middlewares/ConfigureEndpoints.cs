using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Nano35.Identity.Api.Middlewares
{
    public static partial class ConfigureEndpoints
    {
        public static void Configure(IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("please use swagger");
                });
            });
        }
    }
}