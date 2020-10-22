using PRIME_UCR.Domain.Models.Appointments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Services.Appointments
{
    public interface IAppointmentService
    {
        Task<IEnumerable<TipoAccion>> GetActionTypesAsync();
    }
}
