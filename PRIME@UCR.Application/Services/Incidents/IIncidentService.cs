using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Application.Services.Incidents
{
    public interface IIncidentService
    {
        Task<Incidente> GetIncidentAsync(string id);
        Task<IEnumerable<CentroMedico>> GetAllMedicalCentersAsync();
        Task<IEnumerable<Pais>> GetAllCountriesAsync();
        Task<IEnumerable<Provincia>> GetProvincesByCountryAsync(Pais country);
        Task<IEnumerable<Canton>> GetCantonsByProvinceAsync(Provincia province);
        Task<IEnumerable<Distrito>> GetDistrictsByCantonAsync(Canton canton);

    }
}