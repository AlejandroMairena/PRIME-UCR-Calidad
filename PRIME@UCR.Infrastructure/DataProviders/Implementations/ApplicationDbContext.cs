using System.Data;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Domain.Models;
using System.Threading.Tasks;
using PRIME_UCR.Infrastructure.DataProviders.Implementations.EntityConfiguration.Incidents;

namespace PRIME_UCR.Infrastructure.DataProviders.Implementations
{
    public sealed class ApplicationDbContext : DbContext, ISqlDataProvider
    {
        public IDbConnection DbConnection { get; set; }
        public DbSet<Provincia> Provinces { get; set; }
        public DbSet<Pais> Countries { get; set; }
        public DbSet<CentroMedico> MedicalCenters { get; set; }

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
        }

        public Task<int> SaveChangesAsync()
        {
            var result = SaveChanges();
            return Task.FromResult(result);
        }
    }
}
