﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using MimeKit.Text;
using System.Text.Encodings.Web;

namespace PRIME_UCR.Components.UserAdministration
{
    public partial class PasswordRecoveryComponent
    {
        [Parameter]
        public RequestOnPasswordRecoveryModel EmailModel { get; set; }

        [Parameter]
        public EventCallback<RequestOnPasswordRecoveryModel> EmailModelChanged { get; set; }

        [Inject]
        public UserManager<Usuario> UserManager { get; set; }

        [Inject]
        public IMailService MailService { get; set; }

        private bool _isBusy = false;

        public async void CheckUserForRecoveryAsync()
        {
            _isBusy = true;
            var user = await UserManager.FindByNameAsync(EmailModel.Email);
            if(user != null)
            {
                var passwordRecoveryToken = await UserManager.GeneratePasswordResetTokenAsync(user);
                var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(passwordRecoveryToken));
                var firstHalf = ((int)code.Length / 2);
                var code1 = code.Substring(0, firstHalf);
                var code2 = code.Substring(firstHalf, code.Length - firstHalf);
                var emailCoded = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(EmailModel.Email));
                var url = "https://localhost:44368/requestPasswordRecovery/" + emailCoded + "/" + code1 + "/" + code2;
                var emailContent = new EmailContentModel()
                {
                    Destination = EmailModel.Email,
                    Subject = "PRIME@UCR: Recuperar contraseña",
                    Body = $"<h1>PRIME@UCR</h1>  <h2>Restablecer contraseña</h2> <p>Para restablecer la contraseña presione <a href=\"{url}\">aquí</a>. </p>"
                };
                await MailService.SendEmailAsync(emailContent);
                EmailModel.Email = String.Empty;
                EmailModel.Message = "El enlace para recuperar su contraseña ha sido enviado al correo indicado";
                EmailModel.StatusMessage = "success";
            } else
            {
                EmailModel.Message = "El correo ingresado no esta registrado en la aplicación";
                EmailModel.StatusMessage = "danger";
            }
            await EmailModelChanged.InvokeAsync(EmailModel);
            _isBusy = false;
            StateHasChanged();
        }
    }
}
