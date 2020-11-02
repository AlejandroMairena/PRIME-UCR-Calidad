using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Implementations.UserAdministration
{
    public class UsersService : IUserService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        private readonly UserManager<Usuario> userManager;

        private readonly IAuthorizationService authorizationService;

        private readonly AuthenticationStateProvider authenticationStateProvider;

        public UsersService(
            IUsuarioRepository usuarioRepository,
            UserManager<Usuario> _userManager,
            IAuthorizationService _authorizationService,
            AuthenticationStateProvider _authenticationStateProvider)
        {
            _usuarioRepository = usuarioRepository;
            userManager = _userManager;
            authorizationService = _authorizationService;
            authenticationStateProvider = _authenticationStateProvider;
        }

        /**
         * Method used to get the person info by its email
         */
        public async Task<Persona> getPersonWithDetailstAsync(string email)
        {
            var authenticatedUser = (await authenticationStateProvider.GetAuthenticationStateAsync()).User;
            if ((await authorizationService.AuthorizeAsync(authenticatedUser, AuthorizationPolicies.CanManageUsers.ToString())).Succeeded) 
            {
                var user = await userManager.FindByEmailAsync(email);
                var person = await getUsuarioWithDetailsAsync(user.Id);
                return person.Persona;
            }
            return null;
        }

        /**
         * Method used to get an user DTO from a register form DTO
         *
         * Return: A user DTO with the info of the user.
         */
        public async Task<UserFormModel> GetUserFormFromRegisterUserFormAsync(RegisterUserFormModel userToRegister)
        {
            var authenticatedUser = (await authenticationStateProvider.GetAuthenticationStateAsync()).User;
            if ((await authorizationService.AuthorizeAsync(authenticatedUser, AuthorizationPolicies.CanManageUsers.ToString())).Succeeded)
            {
                UserFormModel userModel = new UserFormModel();
                userModel.Email = userToRegister.Email;
                userModel.IdCardNumber = userToRegister.IdCardNumber;
                return userModel;
            }
            return null;
        }

        /**
         * Method used to get all the info of a user given its email.
         * 
         * Return: All the info of the user.
         */
        public async Task<Usuario> getUsuarioWithDetailsAsync(string id)
        {
            return await _usuarioRepository.GetWithDetailsAsync(id);
        }

        /**
         * Method used to get a user from a user DTO.
         * 
         * Return: A user with the info given in the DTO.
         */
        private async Task<Usuario> GetUserFromUserModelAsync(UserFormModel userToRegister)
        {
            var authenticatedUser = (await authenticationStateProvider.GetAuthenticationStateAsync()).User;
            if ((await authorizationService.AuthorizeAsync(authenticatedUser, AuthorizationPolicies.CanManageUsers.ToString())).Succeeded)
            {
                Usuario user = new Usuario();
                user.Email = user.UserName = userToRegister.Email;
                return user;
            }
            return null;
        }

        /**
         * Method used to store a user in the database given all the necessary info of the new user.
         */
        public async Task<bool> StoreUserAsync(UserFormModel userToRegist, string password)
        {
            var authenticatedUser = (await authenticationStateProvider.GetAuthenticationStateAsync()).User;
            if ((await authorizationService.AuthorizeAsync(authenticatedUser, AuthorizationPolicies.CanManageUsers.ToString())).Succeeded)
            {
                var user = await GetUserFromUserModelAsync(userToRegist);
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
            return false;
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
