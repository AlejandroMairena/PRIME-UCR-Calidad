using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Application.Implementations.UserAdministration
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repository;

        public DoctorService(IDoctorRepository repository)
        {
            _repository = repository;
        }

        public async Task<Médico> GetDoctorByIdAsync(string id)
        {
            return await _repository.GetByKeyAsync(id);
        }

        public async Task<IEnumerable<Médico>> GetAllDoctorsAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}