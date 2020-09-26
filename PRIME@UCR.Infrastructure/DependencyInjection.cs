using Microsoft.Extensions.DependencyInjection;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Infrastructure.DataProviders;
using PRIME_UCR.Infrastructure.DataProviders.Implementations;
using PRIME_UCR.Infrastructure.Repositories.Memory;
using PRIME_UCR.Infrastructure.Repositories.Sql;

namespace PRIME_UCR.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            // data providers
            //services.AddScoped(typeof(IMemoryDataProvider<,>), typeof(MemoryDataProvider<,>));
            services.AddTransient<ISqlDataProvider, ApplicationDbContext>();
            // repositories
            //services.AddScoped(typeof(IGenericRepository<,>), typeof(MemoryGenericRepository<,>));
            //services.AddScoped<ITestRepository, MemoryTestRepository>();
            services.AddTransient(typeof(IGenericRepository<,>), typeof(SqlGenericRepository<,>));
            services.AddTransient<ITestRepository, SqlTestRepository>();
            return services;
        }
    }
}