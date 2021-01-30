using System;
using AutoMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;
using Nano35.Identity.Processor.Services.Contexts;
using Nano35.Identity.Processor.Services.MappingProfiles;
using Nano35.Identity.Processor.Services.MassTransit.Consumers;

namespace Nano35.Identity.Processor.Services.AppStart.Configure
{
    public interface IConfigurationOfService
    {
        void AddToServices(IServiceCollection services);
    }
    
    public class Configurator
    {
        private readonly IServiceCollection _services;
        private readonly IConfigurationOfService _configuration;
        public Configurator(
            IServiceCollection services, 
            IConfigurationOfService configuration)
        {
            _services = services;
            _configuration = configuration;
        }

        public void Configure()
        {
            _configuration.AddToServices(_services);
        }
    }
}