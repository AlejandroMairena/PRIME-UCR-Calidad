using Microsoft.Extensions.DependencyInjection;
using PRIME_UCR.Application.Implementations;
using PRIME_UCR.Application.Implementations.CheckLists;
using PRIME_UCR.Application.Services.CheckLists;
using PRIME_UCR.Application.Implementations.Incidents;
using PRIME_UCR.Application.Services;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Application.Services.Multimedia;
using PRIME_UCR.Application.Implementations.Multimedia;

namespace PRIME_UCR.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            // services

            // CheckList services
            services.AddTransient<ICheckListService, CheckListService>();
            services.AddTransient<IItemService, ItemService>();

            services.AddTransient<IIncidentService, IncidentService>();
            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<IMultimediaContentService, MultimediaContentService>();
            services.AddTransient<IEncryptionService, EncryptionService>();
            return services;
        }
    }
}