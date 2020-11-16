using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PRIME_UCR.Application;
using PRIME_UCR.Infrastructure;
using PRIME_UCR.Infrastructure.DataProviders.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using PRIME_UCR.Domain.Models.UserAdministration;
using Microsoft.AspNetCore.Components.Authorization;
using PRIME_UCR.Application.Implementations.UserAdministration;
using Blazored.SessionStorage;
using PRIME_UCR.Validators;
using PRIME_UCR.Application.DTOs.UserAdministration;
using System.Linq;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Constants;

namespace PRIME_UCR
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.LogTo(Console.WriteLine);
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(Configuration.GetConnectionString("DevelopmentDbConnection"));
                //options.UseSqlServer(Configuration.GetConnectionString("ProductionDbConnection"));
            });

            services.AddIdentity<Usuario, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddBlazoredSessionStorage();

            services.AddApplicationLayer();
            services.AddInfrastructureLayer();
            services.AddValidators();

            // authentication
            services.AddScoped<AuthenticationStateProvider,CustomAuthenticationStateProvider>();
            services.AddTransient<IPrimeSecurityService, PrimeSecurityService>();
            services.AddAuthorization(options =>
            {
                foreach(var permission in Enum.GetValues(typeof(AuthorizationPermissions)).Cast<AuthorizationPermissions>())
                {
                    options.AddPolicy(permission.ToString(), policy =>
                        policy.RequireClaim(permission.ToString(), "true"));
                }
            });

            services.AddLocalization();
            var supportedCultures = new List<CultureInfo> { new CultureInfo("es"), new CultureInfo("en"), new CultureInfo("de") };
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("es");
                options.SupportedUICultures = supportedCultures;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRequestLocalization();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
