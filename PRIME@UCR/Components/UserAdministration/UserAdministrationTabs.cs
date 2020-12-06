using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.UserAdministration
{
    public enum UserAdministrationTabs
    {
        [Description("Modificar perfiles")]
        profiles,
        [Description("Crear usuarios")]
        registerUser,
        [Description("Reenviar correo a usuarios no validados")]
        resendValidationEmail
    }
}
