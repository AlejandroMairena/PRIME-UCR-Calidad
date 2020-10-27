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

        public NumeroTelefonoService(INumeroTelefonoRepository _numeroTelefonoRepository)
        {
            numeroTelefonoRepository = _numeroTelefonoRepository;
        }

        public async Task AddNewPhoneNumberAsync(string idUser, string phoneNumber)
        {
            NúmeroTeléfono userPhoneNumber = new NúmeroTeléfono();
            userPhoneNumber.CedPersona = idUser;
            userPhoneNumber.NúmeroTelefónico = phoneNumber;
            await numeroTelefonoRepository.AddPhoneNumberAsync(userPhoneNumber);
        }
    }
}
