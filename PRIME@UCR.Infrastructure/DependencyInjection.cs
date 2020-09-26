using Microsoft.Extensions.DependencyInjection;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Infrastructure.DataProviders;
using PRIME_UCR.Infrastructure.DataProviders.Implementations;
using PRIME_UCR.Infrastructure.Repositories.Sql;
using PRIME_UCR.Infrastructure.Repositories.Sql.Incidents;

namespace PRIME_UCR.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddTransient<ISqlDataProvider, ApplicationDbContext>();
            services.AddTransient(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddTransient<IIncidentRepository, IncidentRepository>();
            return services;
        }
    }
}