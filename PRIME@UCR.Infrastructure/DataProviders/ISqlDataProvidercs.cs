using System.Data;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Domain.Models;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.DataProviders
{
    public interface ISqlDataProvider
    {
        IDbConnection DbConnection { get; set; }
        // dbsets
        DbSet<Provincia> Provinces { get; set; }
        DbSet<Pais> Countries { get; set; }
        DbSet<CentroMedico> MedicalCenters { get; set; }

        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync();     
    }
}