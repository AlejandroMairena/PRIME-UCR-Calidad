using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Application.DTOs.UserAdministration;
using Microsoft.AspNetCore.Identity;
using Blazored.SessionStorage;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Application.Repositories.UserAdministration;
using System.Linq;

namespace PRIME_UCR.Application.Implementations.UserAdministration
{
    /**
     * Class used to manage the authentication of the users to the page.
     */
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService SessionStorageService;

        protected readonly SignInManager<Usuario> SignInManager;

        protected readonly UserManager<Usuario> UserManager;

        private readonly IPrimeAuthorizationService PrimeAuthorizarionService;

        private readonly IUserService UserService;

        private readonly IProfilesService ProfilesService;

        public CustomAuthenticationStateProvider(
            SignInManager<Usuario> _signInManager, 
            UserManager<Usuario> _userManager, 
            ISessionStorageService _sessionStorageService, 
            IPrimeAuthorizationService _primeAuthorizationService, 
            IUserService _userService,
            IProfilesService _profileService)
        {
            SignInManager = _signInManager;
            UserManager = _userManager;
            SessionStorageService = _sessionStorageService;
            PrimeAuthorizarionService = _primeAuthorizationService;
            UserService = _userService;
            ProfilesService = _profileService;
        }

        /*
         * Function:    Used to set the claims to the ClaimsIdentity of the user received in the parameters
         * 
         * Requieres:   The model of the user to which the ClaimsIdentity would be configured
         * Returns:     The ClaimsIdentity of the user received in the parameters
         */
        private ClaimsIdentity GetClaimIdentity(Usuario user, List<Perfil> profilesAndPermissions)
        {
            List<Claim> claimsAuthentication = new List<Claim>();

            claimsAuthentication.Add(new Claim(ClaimTypes.Name, user.Email));
            var profiles = user.UsuariosYPerfiles.FindAll(p => p.IDUsuario == user.Id);

            foreach (var permission in Enum.GetValues(typeof(AuthorizationPermissions)).Cast<AuthorizationPermissions>())
            {
                claimsAuthentication.Add(new Claim(permission.ToString(), PrimeAuthorizarionService.HavePermission((int)permission, profiles, profilesAndPermissions)));
            }

            return new ClaimsIdentity(claimsAuthentication.ToList(), "apiauth_type");
        }

        /*
         * Function:    Used to get the state of the user register in the page if any.
         * 
         * Returns:     An AuthenticationState with the info of the register user if any.
         */
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();

            var emailAddress = await SessionStorageService.GetItemAsync<string>("emailAddress");

            if (emailAddress != null)
            {
                var userToRegister = await UserManager.FindByEmailAsync(emailAddress);
                userToRegister = await UserService.getUsuarioWithDetails(userToRegister.Id);
                var profilesAndPermissions = await ProfilesService.GetPerfilesWithDetailsAsync();
                identity = GetClaimIdentity(userToRegister, profilesAndPermissions);
            }
            
            var user = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(user));
        }

        /*
         * Function:    Used to mark an user as authenticated if the password and email are valids.
         * 
         * Requieres:   A DTO with the information filled in the form of the authentication.
         * Returns:     A boolean indicating if the authentication succeeded.
         */
        public async Task<bool> AuthenticateLogin(LogInModel logInInfo)
        {
            var userToCheck = await UserManager.FindByEmailAsync(logInInfo.Correo);

            ClaimsIdentity identity;
            
            if(userToCheck != null)
            {
                var loginResult = await SignInManager.CheckPasswordSignInAsync(userToCheck, logInInfo.Contraseña, false);

                if (loginResult.Succeeded)
                {
                    //TODO: Check other tables for conflicts with persona
                    userToCheck = await UserService.getUsuarioWithDetails(userToCheck.Id);
                    var profilesAndPermissions = await ProfilesService.GetPerfilesWithDetailsAsync();
                    identity = GetClaimIdentity(userToCheck, profilesAndPermissions);
                } else
                {
                    return await Task.FromResult(false);
                }
            } else
            {
                return await Task.FromResult(false);
            }

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));

            return await Task.FromResult(true);
        }

        /*
         * Funtion:     Used to mark the actual user of the page to logout.
         * 
         * Returns:     A bool indicating if the operation succeeded.
         */
        public async Task<bool> Logout()
        {
            await SessionStorageService.RemoveItemAsync("emailAddress");

            ClaimsIdentity identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));

            return await Task.FromResult(true);
        }

    }
}