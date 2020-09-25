using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Domain.Models;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.DataProviders
{
    public interface ISqlDataProvider
    {
        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync();     
    }
}