using Microsoft.Extensions.DependencyInjection;
using PRIME_UCR.Application.Implementations.Multimedia;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Application.Repositories.Multimedia;
using PRIME_UCR.Application.Services.Multimedia;
using PRIME_UCR.Infrastructure.DataProviders;
using PRIME_UCR.Infrastructure.DataProviders.Implementations;
using PRIME_UCR.Infrastructure.Repositories.Sql;
using PRIME_UCR.Infrastructure.Repositories.Sql.Incidents;
using PRIME_UCR.Infrastructure.Repositories.Sql.Multimedia;

namespace PRIME_UCR.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            // data providers
            services.AddTransient<ISqlDataProvider, ApplicationDbContext>();
            // repositories
            services.AddTransient(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddTransient<ICheckListRepository, SqlCheckListRepository>();
            // generic repositories
            services.AddTransient(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            // incidents repositories
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IProvinceRepository, ProvinceRepository>();
            services.AddTransient<ICantonRepository, CantonRepository>();
            services.AddTransient<IDistrictRepository, DistrictRepository>();
            services.AddTransient<IMedicalCenterRepository, MedicalCenterRepository>();
            services.AddTransient<IModesRepository, ModesRepository>();
            services.AddTransient<IIncidentRepository, IncidentRepository>();
            services.AddTransient<IIncidentStateRepository, IncidentStateRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
            // multimedia
            services.AddTransient<IMultimediaContentRepository, MultimediaContentRepository>();
            services.AddTransient<IFileService, FileService>();
            return services;
        }
    }
}