using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Identity.Processor.Requests.Behaviours;

namespace Nano35.Identity.Processor.Services.AppStart.Configure
{
    public class MediatRConfiguration : 
        IConfigurationOfService
    {
        public void AddToServices(
            IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggerQueryDecorator<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggerCommandDecorator<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionPipeLineBehaviour<,>));
        }
    }
}