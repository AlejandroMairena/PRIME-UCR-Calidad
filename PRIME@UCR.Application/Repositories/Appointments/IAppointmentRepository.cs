using System.Threading.Tasks;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Application.Repositories.Appointments
{
    public interface IAppointmentRepository : IGenericRepository<Cita, int>
    {
    }
}