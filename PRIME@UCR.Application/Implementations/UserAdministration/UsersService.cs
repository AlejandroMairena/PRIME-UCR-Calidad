﻿using Microsoft.AspNetCore.Identity;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Permissions.UserAdministration;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Implementations.UserAdministration
{
    public partial class UsersService : IUserService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        private readonly UserManager<Usuario> userManager;

        private readonly IPrimeSecurityService primeSecurityService;

        public UsersService(
            IUsuarioRepository usuarioRepository,
            UserManager<Usuario> _userManager,
            IPrimeSecurityService _primeSecurityService)
        {
            _usuarioRepository = usuarioRepository;
            userManager = _userManager;
            primeSecurityService = _primeSecurityService;
        }

        /**
         * Method used to get the person info by its email
         */
        public async Task<Persona> getPersonWithDetailstAsync(string email)
        {
            await primeSecurityService.CheckIfIsAuthorizedAsync(this.GetType());
            var user = await userManager.FindByEmailAsync(email);
            var person = await getUsuarioWithDetailsAsync(user?.Id);
            return person?.Persona;
        }

        /**
         * Method used to get an user DTO from a register form DTO
         *
         * Return: A user DTO with the info of the user.
         */
        public async Task<UserFormModel> GetUserFormFromRegisterUserFormAsync(RegisterUserFormModel userToRegister)
        {
            await primeSecurityService.CheckIfIsAuthorizedAsync(this.GetType());
            UserFormModel userModel = new UserFormModel();
            userModel.Email = userToRegister.Email;
            userModel.IdCardNumber = userToRegister.IdCardNumber;
            return userModel;
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
            await primeSecurityService.CheckIfIsAuthorizedAsync(this.GetType());
            Usuario user = new Usuario();
            user.Email = user.UserName = userToRegister.Email;
            return user;
        }

        /**
         * Method used to store a user in the database given all the necessary info of the new user.
         */
        public async Task<bool> StoreUserAsync(UserFormModel userToRegist)
        {
            await primeSecurityService.CheckIfIsAuthorizedAsync(this.GetType());
            var user = await GetUserFromUserModelAsync(userToRegist);
            var existInDB = (await userManager.FindByEmailAsync(user.Email)) == null ? false : true;
            if (!existInDB)
            {
                user.CedPersona = userToRegist.IdCardNumber;
                var result = await userManager.CreateAsync(user);
                return result.Succeeded;
            }
            else
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


        /**
         * Method used to get all the users that aren't validated yet.
         * 
         * Return: A list with all the users that have not been validated in the app.
         */
        public async Task<List<Usuario>> GetNotAuthenticatedUsers()
        {
            return await _usuarioRepository.GetNotAuthenticatedUsers();
        }


    }

    [MetadataType(typeof(UserServiceAuthorization))]
    public partial class UsersService { }


}
