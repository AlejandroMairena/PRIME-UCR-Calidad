using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Application.DTOs.UserAdministration
{
    public class RecoveryPasswordInfoModel
    {
        public RecoveryPasswordInfoModel()
        {
            Email = String.Empty;
            IsValidUser = true;
            PasswordModel = new NewPasswordModel();
        }

        public string PasswordRecoveryToken1 { get; set; }

        public string PasswordRecoveryToken2 { get; set; }

        public string Email { get; set; }

        public bool IsValidUser { get; set; }

        public NewPasswordModel PasswordModel { get; set; }

        public string PasswordRecoveryToken { get
            {
                return PasswordRecoveryToken1 + PasswordRecoveryToken2;
            } }
    }
}
