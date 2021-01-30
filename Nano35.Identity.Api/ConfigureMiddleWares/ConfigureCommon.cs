using Microsoft.AspNetCore.Builder;

namespace Nano35.Identity.Api.ConfigureMiddleWares
{
    public static partial class ConfigureCommon
    {
        public static void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseCors("Cors");
            app.UseMiddleware<UseAuthMiddleware>();
            app.UseRouting(); 
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nano35.Instance.Api");
            });
        }
    }
}