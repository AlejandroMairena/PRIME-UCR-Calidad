using Microsoft.Extensions.DependencyInjection;
using PRIME_UCR.Application.Implementations.Multimedia;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Application.Repositories.Appointments;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Application.Repositories.MedicalRecords;
using PRIME_UCR.Application.Repositories.Multimedia;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.Multimedia;
using PRIME_UCR.Infrastructure.DataProviders;
using PRIME_UCR.Infrastructure.DataProviders.Implementations;
using PRIME_UCR.Infrastructure.Repositories.Sql;
using PRIME_UCR.Infrastructure.Repositories.Sql.Appointments;
using PRIME_UCR.Infrastructure.Repositories.Sql.Incidents;
using PRIME_UCR.Infrastructure.Repositories.Sql.MedicalRecords;
using PRIME_UCR.Infrastructure.Repositories.Sql.Multimedia;
using PRIME_UCR.Infrastructure.Repositories.Sql.UserAdministration;

namespace PRIME_UCR.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            // data providers
            services.AddTransient<ISqlDataProvider, ApplicationDbContext>();
            // repositories
            services.AddTransient(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddTransient<ICheckListRepository, SqlCheckListRepository>();
            // generic repositories
            services.AddTransient(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            // appointments
            services.AddTransient<IActionTypeRepository, ActionTypeRepository>();
            // incidents repositories
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IProvinceRepository, ProvinceRepository>();
            services.AddTransient<ICantonRepository, CantonRepository>();
            services.AddTransient<IDistrictRepository, DistrictRepository>();
            services.AddTransient<IMedicalCenterRepository, MedicalCenterRepository>();
            services.AddTransient<IModesRepository, ModesRepository>();
            services.AddTransient<IIncidentRepository, IncidentRepository>();
            services.AddTransient<IIncidentStateRepository, IncidentStateRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
            // medical records
            services.AddTransient<IMedicalRecordRepository, MedicalRecordRepository>();
            // multimedia
            services.AddTransient<IMultimediaContentRepository, MultimediaContentRepository>();
            services.AddTransient<IFileService, FileService>();

            // user administration repositories
            services.AddTransient<IAdministradorRepository, AdministradorRepository>();
            services.AddTransient<IAdministradorCentroDeControlRepository, AdministradorCentroDeControlRepository>();
            services.AddTransient<ICoordinadorTécnicoMédicoRepository, CoordinadorTécnicoMédicoRepository>();
            services.AddTransient<IEspecialistaTécnicoMédicoRepository, EspecialistaTécnicoMédicoRepository>();
            services.AddTransient<IFuncionarioRepository, FuncionarioRepository>();
            services.AddTransient<IGerenteMédicoRepository, GerenteMédicoRepository>();
            services.AddTransient<IDoctorRepository, DoctorRepository>();
            services.AddTransient<INúmeroTeléfonoRepository, NúmeroTeléfonoRepository>();
            services.AddTransient<IPacienteRepository, PacienteRepository>();
            services.AddTransient<IPerfilRepository, PerfilRepository>();
            services.AddTransient<IPermisoRepository, PermisoRepository>();
            services.AddTransient<IPersonaRepository, PersonaRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();

            return services;
        }
    }
}