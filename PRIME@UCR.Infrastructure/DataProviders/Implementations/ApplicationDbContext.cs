using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Infrastructure.DataProviders.Implementations.EntityConfiguration.UserAdministration;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.DataProviders.Implementations
{
    public class ApplicationDbContext : IdentityDbContext, ISqlDataProvider
    {
        public DbSet<TestModel> TestModels { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TestModel>(tm =>
            {
                tm.HasKey("Key");
            });
            builder.ApplyConfiguration(new UsuarioMap());
        }


        public Task<int> SaveChangesAsync()
        {
            var result = SaveChanges();
            return Task.FromResult(result);
        }
    }
}
