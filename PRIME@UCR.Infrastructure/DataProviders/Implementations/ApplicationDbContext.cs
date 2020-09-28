using System.Data;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Domain.Models;
using System.Threading.Tasks;
using PRIME_UCR.Infrastructure.DataProviders.Implementations.EntityConfiguration.Multimedia;

namespace PRIME_UCR.Infrastructure.DataProviders.Implementations
{
    public sealed class ApplicationDbContext : DbContext, ISqlDataProvider
    {
        public IDbConnection DbConnection { get; set; }
        public DbSet<Acciones> Acciones { get; set; }
        public DbSet<MultimediaContent> Multimedia_Contents { get; set; }
        public DbSet<Cita> Citas { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            //DbConnection = Database.GetDbConnection();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MultimediaContentMap()); 
            builder.ApplyConfiguration(new AccionesMap());
            builder.ApplyConfiguration(new CitaMap());
        }

        public Task<int> SaveChangesAsync()
        {
            var result = SaveChanges();
            return Task.FromResult(result);
        }
    }
}
