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

        [Inject]
        public IPerteneceService perteneceService { get; set; }
        

        private string statusMessage;

        private string messageType;

        public RegisterUserFormModel infoOfUserToRegister;

        private bool isBusy;

        protected override void OnInitialized()
        {
            infoOfUserToRegister = new RegisterUserFormModel();
            isBusy = false;
        }

        /**
         * Method used to register a user once the admin fill the form.
         */
        private async void RegisterUserInDB()
        {
            isBusy = true;
            StateHasChanged();
            var personModel = personService.GetPersonModelFromRegisterModel(infoOfUserToRegister);
            var existPersonInDB = (await personService.GetPersonByIdAsync(personModel.IdCardNumber)) == null ? false : true;
            if (!existPersonInDB)
            {
                await personService.StoreNewPersonAsync(personModel);
                var userModel = await userService.GetUserFormFromRegisterUserFormAsync(infoOfUserToRegister);
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

                    var user = (await userService.GetAllUsersWithDetailsAsync()).ToList().Find(u => u.Email == userModel.Email);

                    foreach (String profileName in infoOfUserToRegister.Profiles)
                    {
                        await perteneceService.InsertUserOfProfileAsync(user.Id, profileName);
                    }
                    infoOfUserToRegister = new RegisterUserFormModel();
                    statusMessage = "El usuario indicado se ha registrado en la aplicación.";
                    messageType = "success";
                }
            } else
            {
                statusMessage = "El usuario indicado ya forma parte de la aplicación.";
                messageType = "danger";
            }
            isBusy = false;
            StateHasChanged();
        }
    }
}
