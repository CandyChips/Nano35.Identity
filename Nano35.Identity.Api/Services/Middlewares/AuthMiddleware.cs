using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Nano35.Identity.Api.Services.Middlewares
{
    public class UseAuthMiddleware
    {    
        private readonly RequestDelegate _next;
        private readonly ILogger<UseAuthMiddleware> _logger;
 
        public UseAuthMiddleware(
            RequestDelegate next, 
            ILogger<UseAuthMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }
 
        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"]!.ToString().Split(' ').Last();
            if (token != "")
            {
                this._logger.Log(LogLevel.Information, token);
            }
            else
            {
                
            }
            await _next.Invoke(context);
        }
    }
}