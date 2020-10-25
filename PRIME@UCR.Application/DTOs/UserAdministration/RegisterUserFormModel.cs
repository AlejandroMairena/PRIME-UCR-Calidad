using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Application.DTOs.UserAdministration
{
    public class RegisterUserFormModel
    {
        public PersonFormModel personForm { get; set; }

        public UserFormModel userForm { get; set; }
    }
}
