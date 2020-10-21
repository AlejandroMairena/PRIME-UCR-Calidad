using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Implementations.UserAdministration
{
    public class PermiteService : IPermiteService
    {
        private readonly IPermiteRepository _IPermiteRepository;

        public PermiteService(IPermiteRepository IPermiteRepository) 
        {
            _IPermiteRepository = IPermiteRepository;
        }

        public async Task DeletePermissionAsync(string idProfile, int idPermission)
        {
            await _IPermiteRepository.DeletePermissionAsync(idProfile, idPermission);
        }
    }
}
