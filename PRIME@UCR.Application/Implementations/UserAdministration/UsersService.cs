using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using PRIME_UCR.Application.DTOs.UserAdministration;
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

        /**
         * Method used to get the person info by its email
         */
        public async Task<Persona> getPersonWithDetailstAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            var person = await getUsuarioWithDetails(user.Id);
            return person.Persona;
        }

        /**
         * Method used to get an user DTO from a register form DTO
         *
         * Return: A user DTO with the info of the user.
         */
        public UserFormModel GetUserFormFromRegisterUserForm(RegisterUserFormModel userToRegister)
        {
            UserFormModel userModel = new UserFormModel();
            userModel.Email = userToRegister.Email;
            userModel.IdCardNumber = userToRegister.IdCardNumber;
            return userModel;
        }

        /**
         * Method used to get all the users of the database.
         *
         * Return: A list with all the users in the database.
         */
        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            return await _usuarioRepository.GetAllAsync();
        }

        /**
         * Method used to get all the info of a user given its email.
         * 
         * Return: All the info of the user.
         */
        public async Task<Usuario> getUsuarioWithDetails(string id)
        {
            return await _usuarioRepository.GetWithDetailsAsync(id);
        }

        /**
         * Method used to get a user from a user DTO.
         * 
         * Return: A user with the info given in the DTO.
         */
        private Usuario GetUserFromUserModel(UserFormModel userToRegister)
        {
            Usuario user = new Usuario();
            user.Email = user.UserName = userToRegister.Email;
            return user;
        }

        /**
         * Method used to store a user in the database given all the necessary info of the new user.
         */
        public async Task<bool> StoreUserAsync(UserFormModel userToRegist, string password)
        {
            var user = GetUserFromUserModel(userToRegist);
            var existInDB = (await userManager.FindByEmailAsync(user.Email)) == null ? false : true;
            if(!existInDB)
            {
                user.CedPersona = userToRegist.IdCardNumber;
                var result = await userManager.CreateAsync(user, password);
                return result.Succeeded;
            } else
            {
                return false;
            }
        }

        /**
         * Method used to get all the users with details.
         * 
         * Return: A list with all the users with the details.
         */
        public async Task<List<Usuario>> GetAllUsersWithDetailsAsync()
        {
            return await _usuarioRepository.GetAllUsersWithDetailsAsync();
        }
    }
}
