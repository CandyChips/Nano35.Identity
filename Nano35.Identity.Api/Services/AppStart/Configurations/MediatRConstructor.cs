using System;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Identity.Api.Services.Requests.Behaviours;

namespace Nano35.Identity.Api.Services.Constructors
{
    public static class MediatRServiceConstructor
    {
        public static void Construct(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipeLineBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipeLineBehaviour<,>));
        }
    }
}