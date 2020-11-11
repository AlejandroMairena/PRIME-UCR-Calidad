using System.Data;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Domain.Models;
using System.Threading.Tasks;
using PRIME_UCR.Domain.Models.Incidents;
using PRIME_UCR.Infrastructure.EntityConfiguration.Incidents;
using PRIME_UCR.Infrastructure.EntityConfiguration.Multimedia;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Infrastructure.EntityConfiguration.UserAdministration;
using PRIME_UCR.Infrastructure.EntityConfiguration.MedicalRecords;
using PRIME_UCR.Domain.Models.Appointments;
using PRIME_UCR.Infrastructure.EntityConfiguration.Appointments;
using PRIME_UCR.Infrastructure.EntityConfiguration.CheckLists;
using PRIME_UCR.Domain.Models.CheckLists;
using PRIME_UCR.Domain.Models.MedicalRecords;

namespace PRIME_UCR.Infrastructure.DataProviders.Implementations
{
    public sealed class ApplicationDbContext : IdentityDbContext, ISqlDataProvider
    {
        public IDbConnection DbConnection { get; set; }
        public DbSet<CheckList> CheckList { get; set; }
        public DbSet<TipoListaChequeo> CheckListTypes { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<InstanceChecklist> InstanceChecklist { get; set; }
        public DbSet<InstanciaItem> InstanciaItems { get; set; }
        public DbSet<InstanciaItemPadre> InstanciaItemPadres { get; set; }
        public DbSet<InstanciaItemHoja> InstanciaItemHojas { get; set; }
        public DbSet<Provincia> Provinces { get; set; }
        public DbSet<Pais> Countries { get; set; }
        public DbSet<Domicilio> HouseholdLocations { get; set; }
        public DbSet<Internacional> InternationalLocations { get; set; }
        public DbSet<CentroMedico> MedicalCenters { get; set; }
        public DbSet<CentroUbicacion> MedicalCenterLocations { get; set; }
        public DbSet<TrabajaEn> WorksOn { get; set; }
        public DbSet<Modalidad> Modes { get; set; }
        public DbSet<UnidadDeTransporte> TransportUnits { get; set; }
        public DbSet<Estado> States { get; set; }
        public DbSet<EstadoIncidente> IncidentStates { get; set; }
        public DbSet<Incidente> Incidents { get; set; }
        public DbSet<Expediente> MedicalRecords { get; set; }
        public DbSet<Canton> Cantons { get; set; }
        public DbSet<Distrito> Districts { get; set; }
        public DbSet<Ubicacion> Locations { get; set; }
        public DbSet<MultimediaContent> Multimedia_Contents { get; set; }
        public DbSet<Cita> Appointments { get; set; }
        public DbSet<Accion> Actions { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Administrador> Adminstrators { get; set; }
        public DbSet<AdministradorCentroDeControl> AdministratorsControlCenter { get; set; }
        public DbSet<CoordinadorTécnicoMédico> MedicalTechnicians { get; set; }
        public DbSet<EspecialistaTécnicoMédico> MedicalSpecialists { get; set; }
        public DbSet<Funcionario> Functionaries { get; set; }
        public DbSet<GerenteMédico> MedicalManagers { get; set; }
        public DbSet<Médico> Doctors { get; set; }
        public DbSet<NúmeroTeléfono> PhoneNumbers { get; set; }
        public DbSet<Paciente> Patients { get; set; }
        public DbSet<Perfil> Profiles { get; set; }
        public DbSet<Permiso> Permissions { get; set; }
        public DbSet<Persona> People { get; set; }
        public DbSet<Pertenece> BelongsTo { get; set; }
        public DbSet<TienePerfil> HasProfile { get; set; }
        public DbSet<Permite> HasPermissionOf { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            DbConnection = Database.GetDbConnection();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CheckListMap());
            builder.ApplyConfiguration(new ItemMap());
            builder.ApplyConfiguration(new TipoListaChequeoMap());
            builder.ApplyConfiguration(new InstanceChecklistMap());
            builder.ApplyConfiguration(new InstanciaItemMap());
            builder.ApplyConfiguration(new InstanciaItemPadreMap());
            builder.ApplyConfiguration(new InstanciaItemHojaMap());
            builder.ApplyConfiguration(new PaisMap());
            builder.ApplyConfiguration(new ProvinciaMap());
            builder.ApplyConfiguration(new DistritoMap());
            builder.ApplyConfiguration(new CantonMap());
            builder.ApplyConfiguration(new CentroMedicoMap());
            builder.ApplyConfiguration(new CentroUbicacionMap());
            builder.ApplyConfiguration(new TrabajaEnMap());
            builder.ApplyConfiguration(new DomicilioMap());
            builder.ApplyConfiguration(new IncidenteMap());
            builder.ApplyConfiguration(new ModalidadMap());
            builder.ApplyConfiguration(new InternacionalMap());
            builder.ApplyConfiguration(new UbicacionMap());
            builder.ApplyConfiguration(new UnidadDeTransporteMap());
            builder.ApplyConfiguration(new EstadoMap());
            builder.ApplyConfiguration(new EstadoIncidenteMap());
            builder.ApplyConfiguration(new MultimediaContentMap());
            builder.ApplyConfiguration(new UsuarioMap());
            builder.ApplyConfiguration(new AdministradorMap());
            builder.ApplyConfiguration(new AdministradorCentroDeControlMap());
            builder.ApplyConfiguration(new CoordinadorTécnicoMédicoMap());
            builder.ApplyConfiguration(new EspecialistaTécnicoMédicoMap());
            builder.ApplyConfiguration(new FuncionarioMap());
            builder.ApplyConfiguration(new GerenteMédicoMap());
            builder.ApplyConfiguration(new MédicoMap());
            builder.ApplyConfiguration(new NúmeroTeléfonoMap());
            builder.ApplyConfiguration(new PacienteMap());
            builder.ApplyConfiguration(new PerfilMap());
            builder.ApplyConfiguration(new PermisoMap());
            builder.ApplyConfiguration(new PersonaMap());
            builder.ApplyConfiguration(new PerteneceMap());
            builder.ApplyConfiguration(new PermiteMap());
            builder.ApplyConfiguration(new TienePerfilMap());
            builder.ApplyConfiguration(new ExpedienteMap());
            builder.ApplyConfiguration(new CitaMap());
            builder.ApplyConfiguration(new AccionMap());
            builder.ApplyConfiguration(new TipoAccionMap());
            builder.ApplyConfiguration(new MetricasMap());
            builder.ApplyConfiguration(new MetricasIncidenteMap());
            builder.ApplyConfiguration(new MetricasCitaMedicaMap());
        }

        public Task<int> SaveChangesAsync()
        {
            var result = SaveChanges();
            return Task.FromResult(result);
        }
    }
}
