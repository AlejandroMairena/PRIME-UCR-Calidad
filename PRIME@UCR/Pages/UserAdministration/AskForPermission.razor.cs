using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Pages.UserAdministration
{
    public partial class AskForPermission
    {
        [Inject]
        public UserManager<Usuario> UserManager { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IUserService userService { get; set; }

        [Inject]
        public IMailService mailService { get; set; }

        public AskForPermissionModel AskForPermissionMod;

        public bool isBusy;

        public char ValidationState = 'N';

        public char ResultOfRecovery = 'N';

        protected override void OnInitialized()
        {
            AskForPermissionMod = new AskForPermissionModel();
            isBusy = false;
        }

        /*
         * Function:        Method used to send the message provided by the user (asking for a permission) to the administrators
         */
        public async Task AskPermission()
        {
            isBusy = true;
            StateHasChanged();
            var listUsers = (await userService.GetAllUsersWithDetailsAsync()).ToList();
            if (AskForPermissionMod.User != null && listUsers != null)
            {
                var userPerson = (await userService.GetAllUsersWithDetailsAsync()).ToList().Find(u => u.Email == AskForPermissionMod.User.Email);
                foreach (var actUser in listUsers)
                {
                    if (actUser.UsuariosYPerfiles.Find(p => p.Perfil.NombrePerfil == "Administrador") == null ? false : true)
                    {
                        var message = new EmailContentModel()
                        {
                            Destination = actUser.Email,
                            Subject = "PRIME@UCR: Ha recibido una solicitud de permiso",
                            Body = $"<p>Estimado(a) {actUser.Persona.Nombre}, el usuario {userPerson.Persona.Nombre} ha solicitado el(los) permiso(s) \"{string.Join(",", AskForPermissionMod.PermissionsList)}\". </p>"
                        };

                        await mailService.SendEmailAsync(message);
                    }

                }
                AskForPermissionMod.StatusMessage = "Su solicitud ha sido enviada.";
                AskForPermissionMod.StatusMessageType = "success";

            }
            else
            {
                ResultOfRecovery = 'F';
            }
            isBusy = false;
            StateHasChanged();

        }
    }
}
