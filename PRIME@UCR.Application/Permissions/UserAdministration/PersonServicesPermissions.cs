using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Constants;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Permissions.UserAdministration
{

    public abstract class PersonServiceAuthorization
    {
        [RequirePermissions(new[] { AuthorizationPermissions.CanSeeBasicDetailsOfIncidents })]
        public abstract Task<Persona> GetPersonByIdAsync(string id);

        [RequirePermissions(new[] { AuthorizationPermissions.CanManageUsers })]
        public abstract Task<PersonFormModel> GetPersonModelFromRegisterModelAsync(RegisterUserFormModel registerUserModel);

        [RequirePermissions(new[] { AuthorizationPermissions.CanManageUsers })]
        public abstract Task StoreNewPersonAsync(PersonFormModel personInfo);

        [RequirePermissions(new[] { AuthorizationPermissions.CanManageUsers })]
        public abstract Task DeletePersonAsync(string id);
    }

}
