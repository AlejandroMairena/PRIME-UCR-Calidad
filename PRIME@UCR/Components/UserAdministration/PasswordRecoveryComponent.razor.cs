using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.DTOs.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.UserAdministration
{
    public partial class PasswordRecoveryComponent
    {
        [Parameter]
        public RequestOnPasswordRecoveryModel EmailModel { get; set; }

        [Parameter]
        public EventCallback<RequestOnPasswordRecoveryModel> EmailModelChanged { get; set; }

        public async void CheckUserForRecoveryAsync()
        {
            EmailModel.Message = "El correo ingresado no esta registrado en la aplicación";
            EmailModel.StatusMessage = "danger";
            await EmailModelChanged.InvokeAsync(EmailModel);
            StateHasChanged();
        }
    }
}
