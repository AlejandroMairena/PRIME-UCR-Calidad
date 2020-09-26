using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Domain.Models;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.DataProviders
{
    public interface ISqlDataProvider
    {
        DbSet<Provincia> Provincias { get; set; }
        DbSet<Pais> Pais { get; set; }
        
        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync();     
    }
}