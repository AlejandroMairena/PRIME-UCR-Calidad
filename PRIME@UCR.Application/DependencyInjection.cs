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

namespace PRIME_UCR.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            /* register services here */
            // appointments
            services.AddTransient<IAppointmentService, AppointmentService>();
            // checklists
            services.AddTransient<ICheckListService, CheckListService>();
            // incidents
            services.AddTransient<IIncidentService, IncidentService>();
            services.AddTransient<ILocationService, LocationService>();
            // medical records
            services.AddTransient<IMedicalRecordService, MedicalRecordService>();
            // multimedia
            services.AddTransient<IMultimediaContentService, MultimediaContentService>();
            services.AddTransient<IEncryptionService, EncryptionService>();
            // user administration
            services.AddTransient<IPermissionsService, PermissionsService>();
            services.AddTransient<IProfilesService, ProfilesService>();
            services.AddTransient<IUserService, UsersService>();
            services.AddScoped<IPrimeAuthorizationService, PrimeAuthorizationService>();
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IDoctorService, DoctorService>();
            return services;
        }
    }
}