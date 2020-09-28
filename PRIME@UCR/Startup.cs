using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PRIME_UCR.Application.Services;
using PRIME_UCR.Application.Implementations;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Application.Services.Multimedia;
using PRIME_UCR.Application.Repositories.Multimedia;
using PRIME_UCR.Infrastructure.Repositories.Sql.Multimedia;
using PRIME_UCR.Application.Implementations.Multimedia;
using PRIME_UCR.Infrastructure.DataProviders;
using PRIME_UCR.Infrastructure.DataProviders.Implementations;
using PRIME_UCR.Infrastructure.Repositories.Sql;
using Microsoft.EntityFrameworkCore;

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

            /*
             * dependency injection summary
             * singleton: shared instance for the whole server
             * transient: shared instance per request to the server(resets on reload)
             * scoped: never shared, one new instance per injection
            */
            //MultimediaContentService - DT
            services.AddTransient<IMultimediaContentRepository, MultimediaContentRepository>();
            services.AddTransient<IMultimediaContentService, MultimediaContentService>();
            // data providers
            services.AddTransient<ISqlDataProvider, ApplicationDbContext>();
            // generic repositories
            services.AddTransient(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DevelopmentDbConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
