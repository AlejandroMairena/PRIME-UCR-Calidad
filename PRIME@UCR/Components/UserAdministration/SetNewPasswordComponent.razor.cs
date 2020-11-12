using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.DTOs.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.UserAdministration
{
    public partial class SetNewPasswordComponent
    {
        [Parameter]
        public RecoveryPasswordInfoModel RecoveryPasswordInfo { get; set; }

        public bool _isBusy = false;

        public void ChangePassword()
        {
            _isBusy = true;

            _isBusy = false;
        }
    }
}
