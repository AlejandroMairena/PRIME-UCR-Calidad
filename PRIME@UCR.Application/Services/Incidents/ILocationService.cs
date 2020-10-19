using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Application.Dtos;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Application.Services.Incidents
{
    public interface ILocationService
    {
        Task<Pais> GetCountryByName(string name);
        Task<IEnumerable<CentroMedico>> GetAllMedicalCentersAsync();
        Task<LocationModel> GetLocationByDistrictId(int districtId);
        Task<IEnumerable<Pais>> GetAllCountriesAsync();
        Task<IEnumerable<Provincia>> GetProvincesByCountryNameAsync(string countryName);
        Task<IEnumerable<Canton>> GetCantonsByProvinceNameAsync(string provinceName);
        Task<IEnumerable<Distrito>> GetDistrictsByCantonIdAsync(int cantonId);
    }
}