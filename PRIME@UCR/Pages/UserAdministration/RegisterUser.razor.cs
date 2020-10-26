using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PRIME_UCR.Pages.UserAdministration
{
    public partial class RegisterUser
    {
        [Inject]
        public IPersonService personService { get; set; } 

        [Inject]
        public IUserService userService { get; set; }

        [Inject]
        public INumeroTelefonoService telefonoService { get; set; }

        private string statusMessage;

        private string messageType;

        public RegisterUserFormModel infoOfUserToRegister;

        protected override void OnInitialized()
        {
            infoOfUserToRegister = new RegisterUserFormModel();
        }

        /**
         * Method used to register a user once the admin fill the form.
         */
        private async void RegisterUserInDB()
        {
            var personModel = personService.GetPersonModelFromRegisterModel(infoOfUserToRegister);
            var existPersonInDB = (await personService.GetPersonByIdAsync(personModel.IdCardNumber)) == null ? false : true;
            if (!existPersonInDB)
            {
                await personService.StoreNewPersonAsync(personModel);
                var userModel = userService.GetUserFormFromRegisterUserForm(infoOfUserToRegister);
                var tempPassword = personModel.Name + "." + personModel.FirstLastName + personModel.PrimaryPhoneNumber;/*Es temporal, luego esto cambiará*/
                var result = await userService.StoreUserAsync(userModel,tempPassword);
                if(!result)
                {
                    await personService.DeletePersonAsync(userModel.IdCardNumber);
                    statusMessage = "El usuario indicado ya forma parte de la aplicación.";
                    messageType = "danger";
                } else
                {
                    await telefonoService.AddNewPhoneNumberAsync(personModel.IdCardNumber, infoOfUserToRegister.PrimaryPhoneNumber);
                    if(!String.IsNullOrEmpty(infoOfUserToRegister.SecondaryPhoneNumber))
                    {
                        await telefonoService.AddNewPhoneNumberAsync(personModel.IdCardNumber, infoOfUserToRegister.SecondaryPhoneNumber);
                    }

                    var user = (await userService.GetUsuarios()).ToList().Find(u => u.Email == userModel.Email);

                    /*Aqui va la parte de registrar el perfil*/
                    /*ELIAN*/
                    
                    statusMessage = "El usuario indicado se ha registrado en la aplicación.";
                    messageType = "success";
                }
                StateHasChanged();
            } else
            {
                statusMessage = "El usuario indicado ya forma parte de la aplicación.";
                messageType = "danger";
                StateHasChanged();
            }

        }
    }
}
