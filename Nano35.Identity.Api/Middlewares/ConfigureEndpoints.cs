using Microsoft.AspNetCore.Builder;

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
                    context.Response.Redirect("/swagger");
                });
            });
        }
    }
}