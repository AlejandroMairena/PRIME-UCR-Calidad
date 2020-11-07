using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Exceptions.UserAdministration;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Implementations.UserAdministration
{
    public class PerteneceService : IPerteneceService
    {
        private readonly IPerteneceRepository _perteneceRepository;

        private readonly IPrimeSecurityService primeSecurityService;

        public PerteneceService(IPerteneceRepository perteneceRepository,
            IPrimeSecurityService _primeSecurityService)
        {
            _perteneceRepository = perteneceRepository;
            primeSecurityService = _primeSecurityService;
        }

        public async Task DeleteUserOfProfileAsync(string idUser, string idProfile)
        {
            await primeSecurityService.CheckIfIsAuthorizedAsync(MethodBase.GetCurrentMethod());
            await _perteneceRepository.DeleteUserFromProfileAsync(idUser, idProfile);
        }

        public async Task InsertUserOfProfileAsync(string idUser, string idProfile)
        {
            await primeSecurityService.CheckIfIsAuthorizedAsync(MethodBase.GetCurrentMethod());
            await _perteneceRepository.InsertUserToProfileAsync(idUser, idProfile);
        }
    }
}
