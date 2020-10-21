using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Implementations.UserAdministration
{
    public class PerteneceService : IPerteneceService
    {
        private readonly IPerteneceRepository _perteneceRepository;

        public PerteneceService(IPerteneceRepository perteneceRepository)
        {
            _perteneceRepository = perteneceRepository;
        }

        public async Task DeleteUserOfProfileAsync(string idUser, string idProfile)
        {
            await _perteneceRepository.DeleteUserFromProfileAsync(idUser, idProfile);
        }

        public async Task InsertUserOfProfileAsync(string idUser, string idProfile)
        {
            await _perteneceRepository.InsertUserToProfileAsync(idUser, idProfile);
        }
    }
}
