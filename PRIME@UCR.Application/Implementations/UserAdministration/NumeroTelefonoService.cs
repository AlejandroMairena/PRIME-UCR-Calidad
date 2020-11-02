using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
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
    public class NumeroTelefonoService : INumeroTelefonoService
    {
        private readonly INumeroTelefonoRepository numeroTelefonoRepository;

        private readonly IAuthorizationService authorizationService;

        private readonly AuthenticationStateProvider authenticationStateProvider;

        public NumeroTelefonoService(INumeroTelefonoRepository _numeroTelefonoRepository,
            AuthenticationStateProvider _authenticationStateProvider,
            IAuthorizationService _authorizationService)
        {
            numeroTelefonoRepository = _numeroTelefonoRepository;
            authorizationService = _authorizationService;
            authenticationStateProvider = _authenticationStateProvider;
        }

        public async Task AddNewPhoneNumberAsync(string idUser, string phoneNumber)
        {
            var user = (await authenticationStateProvider.GetAuthenticationStateAsync()).User;
            if ((await authorizationService.AuthorizeAsync(user, AuthorizationPolicies.CanManageUsers.ToString())).Succeeded)
            {
                NúmeroTeléfono userPhoneNumber = new NúmeroTeléfono();
                userPhoneNumber.CedPersona = idUser;
                userPhoneNumber.NúmeroTelefónico = phoneNumber;
                await numeroTelefonoRepository.AddPhoneNumberAsync(userPhoneNumber);
            }
        }
    }
}
