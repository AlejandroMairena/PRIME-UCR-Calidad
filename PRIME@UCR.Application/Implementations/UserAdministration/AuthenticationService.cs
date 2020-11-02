using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Implementations.UserAdministration
{
    public class AuthenticationService : IAuthenticationService
    {
        protected readonly IUsuarioRepository usuarioRepository;

        protected readonly IPerfilRepository perfilRepository;

        public AuthenticationService(IUsuarioRepository _usuarioRepository
            ,IPerfilRepository _perfilRepository)
        {
            usuarioRepository = _usuarioRepository;
            perfilRepository = _perfilRepository;
        }

        public async Task<List<Perfil>> GetAllProfilesWithDetailsAsync()
        {
            return await perfilRepository.GetPerfilesWithDetailsAsync();
        }

        public async Task<List<Usuario>> GetAllUsersWithDetailsAsync()
        {
            return await usuarioRepository.GetAllUsersWithDetailsAsync();
        }

        public async Task<Usuario> GetUserByEmailAsync(string email)
        {
            return await usuarioRepository.GetUserByEmailAsync(email);
        }

        public async Task<Usuario> GetUserWithDetailsAsync(string id)
        {
            return await usuarioRepository.GetWithDetailsAsync(id);
        }
    }
}
