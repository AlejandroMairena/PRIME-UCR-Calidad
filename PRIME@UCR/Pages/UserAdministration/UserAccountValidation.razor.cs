using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Pages.UserAdministration
{
    public partial class UserAccountValidation
    {
        [Parameter]
        public string EmailEncoded { get; set; }

        [Parameter]
        public string Code1Encoded { get; set; }

        [Parameter]
        public string Code2Encoded { get; set; }

        [Inject]
        public UserManager<Usuario> UserManager { get; set; }

        public UserValidationInfoModel UserValidationInfo;

        public bool InvalidValidation = false;

        protected override async Task OnParametersSetAsync()
        {
            UserValidationInfo = new UserValidationInfoModel();
            UserValidationInfo.EmailEncoded = EmailEncoded;
            UserValidationInfo.Code1Encoded = Code1Encoded;
            UserValidationInfo.Code2Encoded = Code2Encoded;
            await ValidateUserAsync();
        }

        public async Task ValidateUserAsync()
        {
            var user = await UserManager.FindByEmailAsync(UserValidationInfo.Email);
            var result = await UserManager.ConfirmEmailAsync(user, UserValidationInfo.EmailValidationToken);
            if(result.Succeeded)
            {

            }
            else 
            {
                InvalidValidation = true;
            }
        }
    }
}
