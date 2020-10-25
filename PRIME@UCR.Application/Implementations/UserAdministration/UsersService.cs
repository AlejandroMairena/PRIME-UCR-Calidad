using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Implementations.UserAdministration
{
    public class UsersService : IUserService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        private readonly UserManager<Usuario> userManager;

        public UsersService(
            IUsuarioRepository usuarioRepository,
            UserManager<Usuario> _userManager)
        {
            _usuarioRepository = usuarioRepository;
            userManager = _userManager;
        }

        public async Task<Persona> getPersonWithDetailstAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            var person = await getUsuarioWithDetails(user.Id);
            return person.Persona;
        }

        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            return await _usuarioRepository.GetAllAsync();
        }

        public async Task<Usuario> getUsuarioWithDetails(string id)
        {
            return await _usuarioRepository.GetWithDetailsAsync(id);
        }
    }
}
