using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.DTOs.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Pages.UserAdministration
{
    public partial class PasswordRecovery
    {
        [Parameter]
        public string PasswordRecoveryToken1 { get; set; }

        [Parameter]
        public string PasswordRecoveryToken2 { get; set; }

        [Parameter]
        public string Email { get; set; }

        public RecoveryPasswordInfoModel recoveryPasswordInfo;

        protected override void OnInitialized()
        {
            recoveryPasswordInfo = new RecoveryPasswordInfoModel();
        }

        protected override void OnParametersSet()
        {
            recoveryPasswordInfo.Email = Email;
            recoveryPasswordInfo.PasswordRecoveryToken1 = PasswordRecoveryToken1;
            recoveryPasswordInfo.PasswordRecoveryToken2 = PasswordRecoveryToken2;
        }
    }
}
