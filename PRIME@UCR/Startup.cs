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
                options.UseSqlServer(Configuration.GetConnectionString("DevelopmentDbConnection")));
            services.AddIdentity<Usuario, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddBlazoredSessionStorage();
            services.AddScoped<AuthenticationStateProvider,CustomAuthenticationStateProvider>();

            services.AddApplicationLayer();
            services.AddInfrastructureLayer();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanDoAnything", policy =>
                     policy.RequireClaim("CanDoAnything", "true"));

                options.AddPolicy("CanManageUsers", policy =>
                     policy.RequireClaim("CanManageUsers", "true"));

                options.AddPolicy("CanAccessEverythingExceptMedicalData", policy =>
                     policy.RequireClaim("CanAccessEverythingExceptMedicalData", "true"));

                options.AddPolicy("CanAccessIncidentsFromAnMedicalRecordInReadMode", policy =>
                     policy.RequireClaim("CanAccessIncidentsFromAnMedicalRecordInReadMode", "true"));

                options.AddPolicy("CanAccessIncidentsOfHisPatients", policy =>
                     policy.RequireClaim("CanAccessIncidentsOfHisPatients", "true"));

                options.AddPolicy("CanAssignAllStepsOfAIncidents", policy =>
                     policy.RequireClaim("CanAssignAllStepsOfAIncidents", "true"));

                options.AddPolicy("CanAssignPostCreationStepsOfIncidentsAssignedToHim", policy =>
                     policy.RequireClaim("CanAssignPostCreationStepsOfIncidentsAssignedToHim", "true"));

                options.AddPolicy("CanAttachMultimediaInChecklistOfHisPatients", policy =>
                     policy.RequireClaim("CanAttachMultimediaInChecklistOfHisPatients", "true"));

                options.AddPolicy("CanCreateCheckList", policy =>
                     policy.RequireClaim("CanCreateCheckList", "true"));

                options.AddPolicy("CanManageAllIncidents", policy =>
                     policy.RequireClaim("CanManageAllIncidents", "true"));

                options.AddPolicy("CanManageAllMedicalRecords", policy =>
                     policy.RequireClaim("CanManageAllMedicalRecords", "true"));

                options.AddPolicy("CanManageCheckListOfAnIncidentsAssignedToHim", policy =>
                     policy.RequireClaim("CanManageCheckListOfAnIncidentsAssignedToHim", "true"));

                options.AddPolicy("CanManageDashboard", policy =>
                     policy.RequireClaim("CanManageDashboard", "true"));

                options.AddPolicy("CanManageIncidentsAssignedToHim", policy =>
                     policy.RequireClaim("CanManageIncidentsAssignedToHim", "true"));

                options.AddPolicy("CanManageMedicalRecordsOfHisPatients", policy =>
                     policy.RequireClaim("CanManageMedicalRecordsOfHisPatients", "true")); 

                options.AddPolicy("CanOnlyRegisterAnIncident", policy =>
                     policy.RequireClaim("CanOnlyRegisterAnIncident", "true"));

                options.AddPolicy("CanSeeAllInfoOfAnIncidentInReadMode", policy =>
                     policy.RequireClaim("CanSeeAllInfoOfAnIncidentInReadMode", "true"));

                options.AddPolicy("CanSeeMedicalRecordsFromIncidentsInReadMode", policy =>
                     policy.RequireClaim("CanSeeMedicalRecordsFromIncidentsInReadMode", "true"));

                options.AddPolicy("CanSeeMedicalRecordsInReadMode", policy =>
                     policy.RequireClaim("CanSeeMedicalRecordsInReadMode", "true"));

                options.AddPolicy("CanSeeMedicalRecordsOfHisPatients", policy =>
                     policy.RequireClaim("CanSeeMedicalRecordsOfHisPatients", "true"));

                options.AddPolicy("CanSeeMedicalRecordsOfPatientsAssignedToHim", policy =>
                    policy.RequireClaim("CanSeeMedicalRecordsOfPatientsAssignedToHim", "true"));
            });

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
