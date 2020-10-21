using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Application.Repositories.Incidents
{
    public interface IMedicalCenterRepository : IGenericRepository<CentroMedico, int>
    {
        Task<IEnumerable<Médico>> GetDoctorsByMedicalCenterId(int id);
    }
}