using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Domain.Models;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.DataProviders
{
    public interface ISqlDataProvider
    {
        DbSet<TestModel> TestModels { get; set; }
        DbSet<CheckList> CheckList { get; set; }

        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync();     
    }
}