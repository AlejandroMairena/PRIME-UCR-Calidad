using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Application.Repositories.Appointments;
using PRIME_UCR.Application.Services.Appointments;
using PRIME_UCR.Domain.Models.Appointments;

namespace PRIME_UCR.Application.Implementations.Appointments
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IActionTypeRepository _actionTypeRepo;

        public AppointmentService(IActionTypeRepository actionTypeRepo)
        {
            _actionTypeRepo = actionTypeRepo;
        }
        
        public async Task<IEnumerable<TipoAccion>> GetActionTypesAsync()
        {
            return await _actionTypeRepo.GetAllAsync();
        }
    }
}