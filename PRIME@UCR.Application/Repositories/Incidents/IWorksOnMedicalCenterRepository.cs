using PRIME_UCR.Domain.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Application.Repositories.Incidents
{
    public interface IWorksOnMedicalCenterRepository
    {
        Task<IEnumerable<TrabajaEn>> GetAllDoctorsbyMedicalCenterId(int medicalCenterId);
    }
}
