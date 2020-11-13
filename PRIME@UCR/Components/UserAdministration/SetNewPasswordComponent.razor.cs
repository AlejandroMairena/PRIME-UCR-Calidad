using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.UserAdministration
{
    public partial class SetNewPasswordComponent
    {
        [Parameter]
        public RecoveryPasswordInfoModel RecoveryPasswordInfo { get; set; }

        [Inject]
        public UserManager<Usuario> UserManager { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public bool _isBusy = false;

        public char resultOfRecovery;

        public async void ChangePassword()
        {
            _isBusy = true;
            var user = await UserManager.FindByEmailAsync(RecoveryPasswordInfo.Email);
            if(user != null)
            {
                var result = await UserManager.ResetPasswordAsync(user, RecoveryPasswordInfo.PasswordRecoveryToken, RecoveryPasswordInfo.PasswordModel.Password);
                resultOfRecovery = result.Succeeded ? 'T' : 'F';
            } else
            {
                resultOfRecovery = 'F';
            }
            _isBusy = false;
            StateHasChanged();
            if(resultOfRecovery == 'F')
            {
                return;
            }
            await Task.Delay(1500);
            NavigationManager.NavigateTo("/");
        }
    }
}
