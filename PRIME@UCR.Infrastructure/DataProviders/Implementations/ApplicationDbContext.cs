using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Domain.Models;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.DataProviders.Implementations
{
    public class ApplicationDbContext : DbContext, ISqlDataProvider
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
        }


        public Task<int> SaveChangesAsync()
        {
            var result = SaveChanges();
            return Task.FromResult(result);
        }
    }
}
