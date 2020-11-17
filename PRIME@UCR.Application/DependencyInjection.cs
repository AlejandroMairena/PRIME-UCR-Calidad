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
using Microsoft.AspNetCore.Components.Authorization;
using PRIME_UCR.Infrastructure.Repositories.Sql.MedicalRecords;
using PRIME_UCR.Application.Services.Dashboard;
using PRIME_UCR.Application.Implementations.Dashboard;

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
            services.AddTransient<IIncidentService, IncidentService>();
            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<IIncidentService, IncidentService>();
            services.AddTransient<IStateService, StateService>();
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
            services.AddScoped<IPermissionsService, PermissionsService>();
            services.AddScoped<IProfilesService, ProfilesService>();
            services.AddScoped<IUserService, UsersService>();
            services.AddTransient<IPermiteService, PermiteService>();
            services.AddTransient<IPerteneceService, PerteneceService>();
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IDoctorService, DoctorService>();
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IDoctorService, DoctorService>();
            services.AddTransient<INumeroTelefonoService, NumeroTelefonoService>();
            services.AddTransient<IAssignmentService, AssignmentService>();
            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            //Dashboard
            services.AddTransient<IDashboardService, DashboardService>();

            services.AddTransient<IMailService, MailService>();
            return services;
        }
    }
}
