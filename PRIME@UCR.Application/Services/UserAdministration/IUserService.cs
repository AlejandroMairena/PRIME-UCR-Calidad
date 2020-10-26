using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Services.UserAdministration
{
    public interface IUserService
    {
        Task<List<Usuario>> GetAllUsersWithDetailsAsync();

        Task<IEnumerable<Usuario>>GetUsuarios();

        Task<Usuario> getUsuarioWithDetails(string id);

        Task<Persona> getPersonWithDetailstAsync(string email);

        UserFormModel GetUserFormFromRegisterUserForm(RegisterUserFormModel userToRegister);

        Task<bool> StoreUserAsync(UserFormModel userToRegist, string password);
    }
}
