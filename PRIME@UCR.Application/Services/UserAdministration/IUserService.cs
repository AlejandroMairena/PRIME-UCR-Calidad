using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Services.UserAdministration
{
    public interface IUserService
    {
        Task<IEnumerable<Usuario>>GetUsuarios() ;

        Task<Usuario> getUsuarioWithDetails(string id);

        Task<Persona> getPersonWithDetailstAsync(string email);
    }
}
