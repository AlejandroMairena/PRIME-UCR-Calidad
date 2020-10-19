using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Services.UserAdministration
{
    public interface IPersonService
    {
        Task<Persona> getPersonByIdAsync(string id);
    }
}
