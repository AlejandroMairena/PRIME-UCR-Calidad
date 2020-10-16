using System;
using System.Collections.Generic;
using System.Text;
using PRIME_UCR.Domain.Models.UserAdministration;
using System.Threading.Tasks;


namespace PRIME_UCR.Application.Services.UserAdministration
{
    public interface IProfilesService
    {
        Task<IEnumerable<Perfil>> GetPerfiles();
    }
}
