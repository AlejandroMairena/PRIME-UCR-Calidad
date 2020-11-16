using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PRIME_UCR.Application;
using PRIME_UCR.Application.Implementations.UserAdministration;
using PRIME_UCR.Infrastructure;
using PRIME_UCR.Infrastructure.DataProviders.Implementations;

namespace PRIME_UCR.Test.IntegrationTests
{
    public sealed class IntegrationTestWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup: class
    {

        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder(Array.Empty<string>())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<TStartup>();
                });
        }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            var builder = WebHost.CreateDefaultBuilder(Array.Empty<string>());
            builder.UseStartup<TStartup>();
            return builder;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {

                var sp = services.BuildServiceProvider();
                var configuration = sp.GetRequiredService<IConfiguration>();

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.LogTo(Console.WriteLine);
                    options.EnableSensitiveDataLogging();
                    options.UseSqlServer(configuration.GetConnectionString("DevelopmentDbConnection"));
                    //options.UseSqlServer(configuration.GetConnectionString("ProductionDbConnection"));
                });

                services.AddScoped<AuthenticationStateProvider,CustomAuthenticationStateProvider>();

                services.AddApplicationLayer();
                services.AddInfrastructureLayer();
            });
        }
    }
}
