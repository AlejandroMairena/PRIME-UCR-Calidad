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
        DbSet<Pais> Countries { get; set; }
        DbSet<Provincia> Provinces { get; set; }
        DbSet<Canton> Cantons { get; set; }
        DbSet<Distrito> Districts { get; set; }
        DbSet<Ubicacion> Locations { get; set; }
        DbSet<Domicilio> Households { get; set; }
        DbSet<DomicilioUbicacion> HouseholdLocations { get; set; }
        DbSet<PaisUbicacion> InternationalLocations { get; set; }
        DbSet<CentroMedico> MedicalCenters { get; set; }
        DbSet<CentroUbicacion> MedicalCenterLocations { get; set; }
        DbSet<Modalidad> Modes { get; set; }
        DbSet<UnidadDeTransporte> TransportUnits { get; set; }
        DbSet<Estado> States { get; set; }
        DbSet<EstadoIncidente> IncidentStates { get; set; }
        DbSet<Incidente> Incidents { get; set; }

        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync();     
    }
}