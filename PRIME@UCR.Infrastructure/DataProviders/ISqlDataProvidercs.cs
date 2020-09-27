using System.Data;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Domain.Models;
using System.Threading.Tasks;
using PRIME_UCR.Domain.Models.Incidents;

namespace PRIME_UCR.Infrastructure.DataProviders
{
    public interface ISqlDataProvider
    {
        IDbConnection DbConnection { get; set; }
        // dbsets
        DbSet<Provincia> Provinces { get; set; }
        DbSet<Pais> Countries { get; set; }
        DbSet<CentroMedico> MedicalCenters { get; set; }
        // DbSet<Estado> States { get; set; }
        // DbSet<EstadoIncidente> IncidentStates { get; set; }
        // DbSet<Incidente> Incidents { get; set; }
        // DbSet<Canton> Cantons { get; set; }
        // DbSet<Distrito> Districts { get; set; }

        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync();     
    }
}