using System.Data;
using Microsoft.Extensions.DependencyInjection;
using PRIME_UCR.Application.Implementations.Multimedia;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Application.Repositories.Appointments;
using PRIME_UCR.Application.Repositories.CheckLists;
using PRIME_UCR.Application.Repositories.Dashboard;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Application.Repositories.MedicalRecords;
using PRIME_UCR.Application.Repositories.Multimedia;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.Multimedia;
using PRIME_UCR.Infrastructure.DataProviders;
using PRIME_UCR.Infrastructure.DataProviders.Implementations;
using PRIME_UCR.Infrastructure.Repositories.Sql;
using PRIME_UCR.Infrastructure.Repositories.Sql.Appointments;
using PRIME_UCR.Infrastructure.Repositories.Sql.CheckLists;
using PRIME_UCR.Infrastructure.Repositories.Sql.Dashboard;
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
            RepoDb.SqlServerBootstrap.Initialize();

            // data providers
            services.AddTransient<ISqlDataProvider, ApplicationDbContext>();
            // repositories
            // generic repositories
            services.AddTransient(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddTransient(typeof(IRepoDbRepository<,>), typeof(RepoDbRepository<,>));
            // checklists
            services.AddTransient<ICheckListRepository, SqlCheckListRepository>();
            services.AddTransient<IItemRepository, SqlItemRepository>();
            services.AddTransient<IInstanceChecklistRepository, SqlInstanceChecklistRepository>();
            services.AddTransient<IInstanceItemRepository, SqlInstanceItemRepository>();
            // appointments
            services.AddTransient<IActionTypeRepository, ActionTypeRepository>();
            services.AddTransient<IAssignemntRepository, AssignmentRepository>();
            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            services.AddTransient<IUbicationCenterRepository, UbicationCenterRepository>();
            services.AddTransient<IMedCenterRepository, MedCenterRepository>(); 
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
            services.AddTransient<ITransportUnitRepository, TransportUnitRepository>();
            services.AddTransient<IActionTypeRepository, ActionTypeRepository>();
            services.AddTransient<IStateRepository, StateRepository>();
            // medical records
            services.AddTransient<IMedicalRecordRepository, MedicalRecordRepository>();
            services.AddTransient<IMedicalBackgroundRepository, MedicalBackgroundRepository>();
            services.AddTransient<IMedicalBackgroundListRepository, MedicalBackgroundListRepository>();
            services.AddTransient<IAlergyRepository, AlergyRepository>();
            services.AddTransient<IAlergyListRepository, AlergyListRepository>();
            // multimedia
            services.AddTransient<IMultimediaContentRepository, MultimediaContentRepository>();
            services.AddTransient<IActionRepository, ActionRepository>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IMultimediaContentItemRepository, MultimediaContentItemRepository>();

            // user administration repositories
            services.AddTransient<IAdministradorRepository, AdministradorRepository>();
            services.AddTransient<IAdministradorCentroDeControlRepository, AdministradorCentroDeControlRepository>();
            services.AddTransient<ICoordinadorTécnicoMédicoRepository, CoordinadorTécnicoMédicoRepository>();
            services.AddTransient<IEspecialistaTécnicoMédicoRepository, EspecialistaTécnicoMédicoRepository>();
            services.AddTransient<IFuncionarioRepository, FuncionarioRepository>();
            services.AddTransient<IGerenteMédicoRepository, GerenteMédicoRepository>();
            services.AddTransient<IDoctorRepository, DoctorRepository>();
            services.AddTransient<IPacienteRepository, PacienteRepository>();
            services.AddTransient<IPerfilRepository, PerfilRepository>();
            services.AddTransient<IPermisoRepository, PermisoRepository>();
            services.AddTransient<IPersonaRepository, PersonaRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IPermiteRepository, PermiteRepository>();
            services.AddTransient<IPerteneceRepository, PerteneceRepository>();
            services.AddTransient<INumeroTelefonoRepository, NumeroTelefonoRepository>();
            services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();

            //dashboard repositories
            services.AddTransient<IDashboardRepository, DashboardRepository>();

            // temporary file service with no encryption
            services.AddTransient<ITempFileServiceNoEncryption, TempFileServiceNoEncryption>();
            return services;
        }
    }
}