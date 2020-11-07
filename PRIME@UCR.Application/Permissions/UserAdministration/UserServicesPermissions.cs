using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Constants;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Application.Permissions.UserAdministration
{ 
    [AuthorizationType(typeof(UserServiceAuthorization))]
    public partial class UserService
    {
    }
    
    public abstract class UserServiceAuthorization
    {
        [RequirePermissions(new[]{ AuthorizationPermissions.CanModifyUsers })] 
        public abstract Task<Persona> getPersonWithDetailstAsync(string email);

        [RequirePermissions(new[] { AuthorizationPermissions.CanCreateUsers, AuthorizationPermissions.CanModifyUsers })]
        public abstract Task<UserFormModel> GetUserFormFromRegisterUserFormAsync(RegisterUserFormModel userToRegister);

        [RequirePermissions(new[] { AuthorizationPermissions.CanCreateUsers, AuthorizationPermissions.CanModifyUsers })]
        public abstract Task<Usuario> GetUserFromUserModelAsync(UserFormModel userToRegister);

        [RequirePermissions(new[] { AuthorizationPermissions.CanCreateUsers })]
        public abstract Task<bool> StoreUserAsync(UserFormModel userToRegist, string password);
    }
}
