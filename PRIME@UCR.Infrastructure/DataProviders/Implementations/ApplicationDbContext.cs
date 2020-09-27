using System.Data;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Domain.Models;
using System.Threading.Tasks;
using PRIME_UCR.Domain.Models.Incidents;
using PRIME_UCR.Infrastructure.DataProviders.Implementations.EntityConfiguration.Incidents;

namespace PRIME_UCR.Infrastructure.DataProviders.Implementations
{
    public sealed class ApplicationDbContext : DbContext, ISqlDataProvider
    {
        public IDbConnection DbConnection { get; set; }
        public DbSet<Provincia> Provinces { get; set; }
        public DbSet<Pais> Countries { get; set; }
        public DbSet<PaisUbicacion> InternationalLocations { get; set; }
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
        public DbSet<Domicilio> Households { get; set; }
        public DbSet<DomicilioUbicacion> HouseholdLocations { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            DbConnection = Database.GetDbConnection();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PaisMap());
            builder.ApplyConfiguration(new ProvinciaMap());
            builder.ApplyConfiguration(new DistritoMap());
            builder.ApplyConfiguration(new CantonMap());
            builder.ApplyConfiguration(new CentroMedicoMap());
            builder.ApplyConfiguration(new CentroUbicacionMap());
            builder.ApplyConfiguration(new DomicilioMap());
            builder.ApplyConfiguration(new DomicilioUbicacionMap());
            builder.ApplyConfiguration(new IncidenteMap());
            builder.ApplyConfiguration(new ModalidadMap());
            builder.ApplyConfiguration(new PaisUbicacionMap());
            builder.ApplyConfiguration(new UbicacionMap());
            builder.ApplyConfiguration(new UnidadDeTransporteMap());
            builder.ApplyConfiguration(new EstadoMap());
            builder.ApplyConfiguration(new EstadoIncidenteMap());
        }

        public Task<int> SaveChangesAsync()
        {
            var result = SaveChanges(); // TODO: check if async works
            return Task.FromResult(result);
        }
    }
}
