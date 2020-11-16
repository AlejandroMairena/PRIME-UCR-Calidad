using PRIME_UCR.Application.Permissions.Incidents;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.Incidents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Implementations.Incidents
{
    public partial class StateService : IStateService
    {
        private readonly IStateRepository _stateRepository;
        private readonly IPrimeSecurityService _primeSecurityService;
        public StateService(IStateRepository stateRepository, IPrimeSecurityService primeSecurityService)
        {
            _stateRepository = stateRepository;
            _primeSecurityService = primeSecurityService;
        }
        public async Task<IEnumerable<Estado>> GetAllStates()
        {
            await _primeSecurityService.CheckIfIsAuthorizedAsync(this.GetType());
            return await _stateRepository.GetAllStates();
        }
    }
    [MetadataType(typeof(StateServicePermissions))]
    public partial class StateService { }

}
