using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Application.Dtos;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.UserAdministration;
using System.Linq;

namespace PRIME_UCR.Application.Implementations.Incidents
{
    public class LocationService : ILocationService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IProvinceRepository _provinceRepository;
        private readonly ICantonRepository _cantonRepository;
        private readonly IDistrictRepository _districtRepository;
        private readonly IMedicalCenterRepository _medicalCenterRepository;
        private readonly IWorksOnMedicalCenterRepository _worksOnMedicalCenterRepository;

        public LocationService(
            ICountryRepository countryRepository,
            IProvinceRepository provinceRepository,
            ICantonRepository cantonRepository,
            IDistrictRepository districtRepository,
            IMedicalCenterRepository medicalCenterRepository,
            IWorksOnMedicalCenterRepository worksOnMedicalCenterRepository)
        {
            _countryRepository = countryRepository;
            _provinceRepository = provinceRepository;
            _cantonRepository = cantonRepository;
            _districtRepository = districtRepository;
            _medicalCenterRepository = medicalCenterRepository;
            _worksOnMedicalCenterRepository = worksOnMedicalCenterRepository;
        }

        public async Task<IEnumerable<TrabajaEn>> GetAllDoctorsbyMedicalCenter(int medicalCenterId)
        {
            return await _worksOnMedicalCenterRepository.GetAllDoctorsbyMedicalCenterId(medicalCenterId);
        }

        public async Task<IEnumerable<CentroMedico>> GetAllMedicalCentersAsync()
        {
            return await _medicalCenterRepository.GetAllAsync();
        }

        public async Task<LocationModel> GetLocationByDistrictId(int districtId)
        {

            var district =
                await _districtRepository.GetDistrictWithFullLocationById(districtId);
            
            return new LocationModel()
            {
                Country = district.Canton.Provincia.Pais,
                Province = district.Canton.Provincia,
                Canton = district.Canton,
                District = district
            };
        }

        public async Task<IEnumerable<Pais>> GetAllCountriesAsync()
        {
            return await _countryRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Provincia>> GetProvincesByCountryNameAsync(string countryName)
        {
            return await _provinceRepository.GetProvincesByCountryNameAsync(countryName);
        }

        public async Task<IEnumerable<Canton>> GetCantonsByProvinceNameAsync(string provinceName)
        {
            return await _cantonRepository.GetCantonsByProvinceNameAsync(provinceName);
        }

        public async Task<IEnumerable<Distrito>> GetDistrictsByCantonIdAsync(int cantonId)
        {
            return await _districtRepository.GetDistrictsByCantonIdAsync(cantonId);
        }
    }
}