using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Implementations.UserAdministration
{
    public class PersonService : IPersonService
    {

        private readonly IPersonaRepository PersonRepository;

        public PersonService(IPersonaRepository _personaRepository)
        {
            PersonRepository = _personaRepository;
        }

        public async Task<Persona> GetPersonByIdAsync(string id)
        {
            return await PersonRepository.GetByKeyPersonaAsync(id);
        }
    }
}
