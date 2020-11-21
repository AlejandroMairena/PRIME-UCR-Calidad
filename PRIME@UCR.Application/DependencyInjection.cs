using Microsoft.Extensions.DependencyInjection;
using PRIME_UCR.Application.Implementations;
using PRIME_UCR.Application.Implementations.Appointments;
using PRIME_UCR.Application.Implementations.CheckLists;
using PRIME_UCR.Application.Services.CheckLists;
using PRIME_UCR.Application.Implementations.Incidents;
using PRIME_UCR.Application.Implementations.MedicalRecords;
using PRIME_UCR.Application.Services;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Application.Services.Multimedia;
using PRIME_UCR.Application.Implementations.Multimedia;
using PRIME_UCR.Application.Implementations.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Application.Services.Appointments;
using PRIME_UCR.Application.Services.MedicalRecords;
using PRIME_UCR.Application.Services.Dashboard;
using PRIME_UCR.Application.Implementations.Dashboard;
using Microsoft.AspNetCore.Components.Authorization;
using PRIME_UCR.Infrastructure.Repositories.Sql.MedicalRecords;
using PRIME_UCR.Application.Services.Dashboard;
using PRIME_UCR.Application.Implementations.Dashboard;
using PRIME_UCR.Application.Permissions.UserAdministration;
using PRIME_UCR.Application.Permissions.Incidents;

namespace PRIME_UCR.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            // services
            services.AddTransient<ICheckListService, CheckListService>();
            services.AddTransient<IInstanceChecklistService, InstanceChecklistService>();
            // incidents
            services.AddTransient<IAssignmentService, SecureAssignmentService>();
            services.AddTransient<IIncidentService, SecureIncidentService>();
            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<IStateService, SecureStateService>();

            // medical records
            services.AddTransient<IMedicalRecordService, MedicalRecordService>();
            services.AddTransient<IMedicalBackgroundService, MedicalBackgroundService>();
            services.AddTransient<IAlergyService, AlergyService>();
            services.AddTransient<IChronicConditionService, ChronicConditionService>();
            services.AddTransient<IAppointmentService, AppointmentService>();
            // multimedia
            services.AddTransient<IMultimediaContentService, MultimediaContentService>();
            services.AddTransient<IEncryptionService, EncryptionService>();
            // user administration
            services.AddScoped<IPermissionsService, SecurePermissionService>();
            services.AddScoped<IProfilesService, SecureProfilesService>();
            services.AddScoped<IUserService, SecureUserService>();
            services.AddTransient<IPermiteService, SecurePermiteService>();
            services.AddTransient<IDoctorService, SecureDoctorService>();
            services.AddTransient<IPerteneceService, SecurePerteneceService>();
            services.AddTransient<IPersonService, SecurePersonService>();
            services.AddTransient<IPatientService, SecurePatientService>();
            services.AddTransient<INumeroTelefonoService, SecureNumeroTelefonoService>();


            services.AddTransient<IDashboardService, DashboardService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            //Dashboard
            services.AddTransient<IDashboardService, DashboardService>();

            services.AddTransient<IMailService, MailService>();
            return services;
        }
    }
}
