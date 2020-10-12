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
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService SessionStorageService;

        protected readonly SignInManager<Usuario> SignInManager;

        protected readonly UserManager<Usuario> UserManager;

        private readonly IPrimeAuthorizationService PrimeAuthorizarionService;

        public CustomAuthenticationStateProvider(SignInManager<Usuario> _signInManager, UserManager<Usuario> _userManager, ISessionStorageService _sessionStorageService, IPrimeAuthorizationService _primeAuthorizationService)
        {
            SignInManager = _signInManager;
            UserManager = _userManager;
            SessionStorageService = _sessionStorageService;
            PrimeAuthorizarionService = _primeAuthorizationService;
        }

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

        public async Task<bool> AuthenticateLogin(LogInModel logInInfo)
        {
            var userToCheck = await UserManager.FindByEmailAsync(logInInfo.Correo);

            ClaimsIdentity identity = new ClaimsIdentity();
            
            if(userToCheck != null)
            {
                var loginResult = await SignInManager.CheckPasswordSignInAsync(userToCheck, logInInfo.Contraseña, false);

                if (loginResult.Succeeded)
                {
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