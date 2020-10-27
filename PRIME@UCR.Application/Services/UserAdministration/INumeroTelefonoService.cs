using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Services.UserAdministration
{
    public interface INumeroTelefonoService
    {
        Task AddNewPhoneNumberAsync(string idUser, string phoneNumber);
    }
}
