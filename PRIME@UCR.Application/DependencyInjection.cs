using Microsoft.Extensions.DependencyInjection;
using PRIME_UCR.Application.Implementations;
using PRIME_UCR.Application.Implementations.CheckLists;
using PRIME_UCR.Application.Services.CheckLists;
using PRIME_UCR.Application.Implementations.Incidents;
using PRIME_UCR.Application.Services;
using PRIME_UCR.Application.Services.Incidents;

namespace PRIME_UCR.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            // services

            services.AddScoped<ICheckListService, CheckListService>();
            services.AddScoped<IIncidentService, IncidentService>();
            services.AddScoped<ILocationService, LocationService>();
            return services;
        }
    }
}