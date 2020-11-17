using System.Threading.Tasks;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Application.Repositories.Appointments
{
    public interface IAppointmentRepository : IRepoDbRepository<Cita, int>
    {
        Task<Cita> getLatestAppointmentByRecordId(int id);
    }
}