using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Repositories.UserAdministration
{
    public interface IPersonaRepository : IGenericRepository<Persona, string>
    {
        Task<Persona> GetByKeyPersonaAsync(string id);

        Task<Persona> GetWithDetailsAsync(string id);
    }
}
