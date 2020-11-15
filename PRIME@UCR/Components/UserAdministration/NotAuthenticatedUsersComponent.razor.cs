using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.UserAdministration
{
    public partial class NotAuthenticatedUsersComponent
    {
        [Inject]
        public IUserService userService { get; set; }

        [Inject]
        public IMailService mailService { get; set; }

        [Inject]
        public UserManager<Usuario> userManager { get; set; }

        public List<Usuario> ListNotAuthenticatedUsers { get; set; }

        private string statusMessage = String.Empty;

        private string messageType = String.Empty;

        protected override void OnInitialized()
        {
            ListNotAuthenticatedUsers = new List<Usuario>();
        }

        protected override async Task OnInitializedAsync()
        {
            ListNotAuthenticatedUsers = (await userService.GetNotAuthenticatedUsers()).ToList();
        }

        public async void resendEMailConfirmation(string userEmail)
        {
            var user = (await userService.GetAllUsersWithDetailsAsync()).ToList().Find(u => u.Email == userEmail);

            var emailConfirmedToken = await userManager.GenerateEmailConfirmationTokenAsync(user);

            var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(emailConfirmedToken));
            var firstHalf = ((int)code.Length / 2);
            var code1 = code.Substring(0, firstHalf);
            var code2 = code.Substring(firstHalf, code.Length - firstHalf);
            var emailCoded = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(user.Email));
            var url = "https://localhost:44368/validateUserAccount/" + emailCoded + "/" + code1 + "/" + code2;
            var message = new EmailContentModel()
            {
                Destination = user.Email,
                Subject = "PRIME@UCR: Validación nueva cuenta de usuario",
                Body = $"<h1>PRIME@UCR</h1>  <h2>Validación de cuenta de usuario.</h2> <p>Para validar su cuenta, presione <a href=\"{url}\">acá</a>. </p>"
            };

            await mailService.SendEmailAsync(message);
            statusMessage = "Se ha reenviado un correo de validación de cuenta al usuario indicado.";
            messageType = "success";
            StateHasChanged();

        }
    }
}
