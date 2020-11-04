using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Exceptions.UserAdministration;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.UserAdministration
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly ISqlDataProvider _db;

        private readonly IPrimeSecurityService primeSecurityService;

        public PersonaRepository(ISqlDataProvider dataProvider, 
            IPrimeSecurityService _primeSecurityService)
        {
            _db = dataProvider;
            primeSecurityService = _primeSecurityService;
        }

        public async Task DeleteAsync(string cedPersona)
        {
            if ((await primeSecurityService.isAuthorizedAsync(AuthorizationPolicies.CanManageUsers)))
            {
                var person = await _db.People.FindAsync(cedPersona);
                if(person != null)
                {
                    _db.People.Remove(person);
                }
                await _db.SaveChangesAsync();
            }
            else
            {
                throw new NotAuthorizedException();
            }
        }

        public async Task<Persona> GetByKeyPersonaAsync(string id)
        {
            if ((await primeSecurityService.isAuthorizedAsync(AuthorizationPolicies.CanManageUsers)))
            {
                return await _db.People.FindAsync(id);
            } else
            {
                throw new NotAuthorizedException();
            }
        }

        public async Task<Persona> GetWithDetailsAsync(string id)
        {
            if ((await primeSecurityService.isAuthorizedAsync(AuthorizationPolicies.CanManageUsers)))
            {
                return await _db.People
                    .Include(i => i.Cédula)
                    .Include(i => i.Nombre)
                    .Include(i => i.PrimerApellido)
                    .Include(i => i.SegundoApellido)
                    .Include(i => i.Sexo)
                    .FirstOrDefaultAsync(i => i.Cédula == id);
            }
            else
            {
                throw new NotAuthorizedException();
            }
        }

        public async Task InsertAsync(Persona persona)
        {
            if ((await primeSecurityService.isAuthorizedAsync(AuthorizationPolicies.CanManageUsers)))
            {
                _db.People.Add(persona);
                await _db.SaveChangesAsync();
            }
            else
            {
                throw new NotAuthorizedException();
            }
        }
    }
}
