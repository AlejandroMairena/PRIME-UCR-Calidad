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

        private readonly IPersonService PersonService;

        public CustomAuthenticationStateProvider(SignInManager<Usuario> _signInManager, UserManager<Usuario> _userManager, ISessionStorageService _sessionStorageService, IPrimeAuthorizationService _primeAuthorizationService, IPersonService _personService)
        {
            SignInManager = _signInManager;
            UserManager = _userManager;
            SessionStorageService = _sessionStorageService;
            PrimeAuthorizarionService = _primeAuthorizationService;
            PersonService = _personService;
        }

        /*
         * Function:    Used to set the claims to the ClaimsIdentity of the user received in the parameters
         * 
         * Requieres:   The model of the user to which the ClaimsIdentity would be configured
         * Returns:     The ClaimsIdentity of the user received in the parameters
         */
        private ClaimsIdentity GetClaimIdentity(Usuario user)
        {
            return new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("CanDoAnything", PrimeAuthorizarionService.CanDoAnything()),
                new Claim("CanManageUsers", PrimeAuthorizarionService.CanManageUsers()),
                new Claim("CanAccessEverythingExceptMedicalData", PrimeAuthorizarionService.CanAccessEverythingExceptMedicalData()),
                new Claim("CanAccessIncidentsFromAnMedicalRecordInReadMode", PrimeAuthorizarionService.CanAccessIncidentsFromAnMedicalRecordInReadMode()),
                new Claim("CanAccessIncidentsOfHisPatients", PrimeAuthorizarionService.CanAccessIncidentsOfHisPatients()),
                new Claim("CanAssignAllStepsOfAIncidents", PrimeAuthorizarionService.CanAssignAllStepsOfAIncidents()),
                new Claim("CanAssignPostCreationStepsOfIncidentsAssignedToHim", PrimeAuthorizarionService.CanAssignPostCreationStepsOfIncidentsAssignedToHim()),
                new Claim("CanAttachMultimediaInChecklistOfHisPatients", PrimeAuthorizarionService.CanAttachMultimediaInChecklistOfHisPatients()),
                new Claim("CanCreateCheckList", PrimeAuthorizarionService.CanCreateCheckList()),
                new Claim("CanManageAllIncidents", PrimeAuthorizarionService.CanManageAllIncidents()),
                new Claim("CanManageAllMedicalRecords", PrimeAuthorizarionService.CanManageAllMedicalRecords()),
                new Claim("CanManageCheckListOfAnIncidentsAssignedToHim", PrimeAuthorizarionService.CanManageCheckListOfAnIncidentsAssignedToHim()),
                new Claim("CanManageDashboard", PrimeAuthorizarionService.CanManageDashboard()),
                new Claim("CanManageIncidentsAssignedToHim", PrimeAuthorizarionService.CanManageIncidentsAssignedToHim()),
                new Claim("CanManageMedicalRecordsOfHisPatients", PrimeAuthorizarionService.CanManageMedicalRecordsOfHisPatients()),
                new Claim("CanOnlyRegisterAnIncident", PrimeAuthorizarionService.CanOnlyRegisterAnIncident()),
                new Claim("CanSeeAllInfoOfAnIncidentInReadMode", PrimeAuthorizarionService.CanSeeAllInfoOfAnIncidentInReadMode()),
                new Claim("CanSeeMedicalRecordsFromIncidentsInReadMode", PrimeAuthorizarionService.CanSeeMedicalRecordsFromIncidentsInReadMode()),
                new Claim("CanSeeMedicalRecordsInReadMode", PrimeAuthorizarionService.CanSeeMedicalRecordsInReadMode()),
                new Claim("CanSeeMedicalRecordsOfHisPatients", PrimeAuthorizarionService.CanSeeMedicalRecordsOfHisPatients()),
                new Claim("CanSeeMedicalRecordsOfPatientsAssignedToHim", PrimeAuthorizarionService.CanSeeMedicalRecordsOfPatientsAssignedToHim()),
            }, "apiauth_type");
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
                identity = GetClaimIdentity(userToRegister);
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

            ClaimsIdentity identity = new ClaimsIdentity();
            
            if(userToCheck != null)
            {
                var loginResult = await SignInManager.CheckPasswordSignInAsync(userToCheck, logInInfo.Contraseña, false);

                if (loginResult.Succeeded)
                {
                    userToCheck.Persona = await PersonService.getPersonByIdAsync(userToCheck.CedPersona);   
                    identity = GetClaimIdentity(userToCheck);
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