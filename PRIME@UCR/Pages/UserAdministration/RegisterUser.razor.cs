using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Pages.UserAdministration
{
    public partial class RegisterUser
    {
        [Inject]
        public IPersonService personService { get; set; } 

        [Inject]
        public IUserService userService { get; set; }

        public RegisterUserFormModel infoOfUserToRegister;

        protected override void OnInitialized()
        {
            infoOfUserToRegister = new RegisterUserFormModel();
        }

        private void RegisterUserInDB()
        {
            infoOfUserToRegister.Sex = 'O';
        }
    }
}
