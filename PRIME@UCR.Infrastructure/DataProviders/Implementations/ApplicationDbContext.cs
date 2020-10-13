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
using PRIME_UCR.Infrastructure.EntityConfiguration.CheckLists;
using PRIME_UCR.Domain.Models.CheckLists;

namespace PRIME_UCR.Infrastructure.DataProviders.Implementations
{
    public sealed class ApplicationDbContext : IdentityDbContext, ISqlDataProvider
    {
        public IDbConnection DbConnection { get; set; }
        public DbSet<CheckList> CheckList { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Provincia> Provinces { get; set; }
        public DbSet<Pais> Countries { get; set; }
        public DbSet<Domicilio> HouseholdLocations { get; set; }
        public DbSet<Internacional> InternationalLocations { get; set; }
        public DbSet<CentroMedico> MedicalCenters { get; set; }
        public DbSet<CentroUbicacion> MedicalCenterLocations { get; set; }
        public DbSet<Modalidad> Modes { get; set; }
        public DbSet<UnidadDeTransporte> TransportUnits { get; set; }
        public DbSet<Estado> States { get; set; }
        public DbSet<EstadoIncidente> IncidentStates { get; set; }
        public DbSet<Incidente> Incidents { get; set; }
        public DbSet<Canton> Cantons { get; set; }
        public DbSet<Distrito> Districts { get; set; }
        public DbSet<Ubicacion> Locations { get; set; }
        public DbSet<MultimediaContent> Multimedia_Contents { get; set; }
        public DbSet<Usuario> Usuarios { get ; set ; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            DbConnection = Database.GetDbConnection();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CheckListMap());
            builder.ApplyConfiguration(new PaisMap());
            builder.ApplyConfiguration(new ProvinciaMap());
            builder.ApplyConfiguration(new DistritoMap());
            builder.ApplyConfiguration(new CantonMap());
            builder.ApplyConfiguration(new CentroMedicoMap());
            builder.ApplyConfiguration(new CentroUbicacionMap());
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
        }

        public Task<int> SaveChangesAsync()
        {
            var result = SaveChanges();
            return Task.FromResult(result);
        }
    }
}
