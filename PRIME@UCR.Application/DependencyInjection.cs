using Microsoft.Extensions.DependencyInjection;
using PRIME_UCR.Application.Implementations;
using PRIME_UCR.Application.Implementations.CheckLists;
using PRIME_UCR.Application.Services;
using PRIME_UCR.Application.Services.CheckLists;

namespace PRIME_UCR.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            // services
            services.AddScoped<ITestService, TestService>();

            services.AddScoped<ICheckListService, CheckListService>();
            return services;
        }
    }
}