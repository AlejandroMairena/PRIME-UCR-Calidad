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
        public string PasswordRecoveryToken1Encoded { get; set; }

        [Parameter]
        public string PasswordRecoveryToken2Encoded { get; set; }

        [Parameter]
        public string EmailEncoded { get; set; }

        public RecoveryPasswordInfoModel recoveryPasswordInfo;

        protected override void OnInitialized()
        {
            recoveryPasswordInfo = new RecoveryPasswordInfoModel();
        }

        protected override void OnParametersSet()
        {
            recoveryPasswordInfo.EmailEncoded = EmailEncoded;
            recoveryPasswordInfo.PasswordRecoveryToken1Encoded = PasswordRecoveryToken1Encoded;
            recoveryPasswordInfo.PasswordRecoveryToken2Encoded = PasswordRecoveryToken2Encoded;
        }
    }
}
