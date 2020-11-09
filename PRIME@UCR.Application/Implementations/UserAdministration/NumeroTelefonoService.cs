using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Exceptions.UserAdministration;
using PRIME_UCR.Application.Permissions.UserAdministration;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Implementations.UserAdministration
{
    public partial class NumeroTelefonoService : INumeroTelefonoService
    {
        private readonly INumeroTelefonoRepository numeroTelefonoRepository;

        private readonly IPrimeSecurityService primeSecurityService;

        public NumeroTelefonoService(INumeroTelefonoRepository _numeroTelefonoRepository,
            IPrimeSecurityService _primeSecurityService)
        {
            numeroTelefonoRepository = _numeroTelefonoRepository;
            primeSecurityService = _primeSecurityService;
        }

        public async Task AddNewPhoneNumberAsync(string idUser, string phoneNumber)
        {
            await primeSecurityService.CheckIfIsAuthorizedAsync(this.GetType());
            NúmeroTeléfono userPhoneNumber = new NúmeroTeléfono();
            userPhoneNumber.CedPersona = idUser;
            userPhoneNumber.NúmeroTelefónico = phoneNumber;
            await numeroTelefonoRepository.AddPhoneNumberAsync(userPhoneNumber);
        }
    }

    [MetadataType(typeof(NumeroTelefonoServiceAuthorization))]
    public partial class NumeroTelefonoService { }
}
