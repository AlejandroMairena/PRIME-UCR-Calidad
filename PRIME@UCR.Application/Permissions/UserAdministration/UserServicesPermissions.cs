﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Constants;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Application.Permissions.UserAdministration
{ 
    
    public abstract class UserServiceAuthorization
    {
        [RequirePermissions(new[]{ AuthorizationPermissions.CanSeeBasicDetailsOfIncidents })] 
        public abstract Task<Persona> getPersonWithDetailstAsync(string email);

        [RequirePermissions(new[] { AuthorizationPermissions.CanManageUsers })]
        public abstract Task<UserFormModel> GetUserFormFromRegisterUserFormAsync(RegisterUserFormModel userToRegister);

        [RequirePermissions(new[] { AuthorizationPermissions.CanManageUsers })]
        public abstract Task<Usuario> GetUserFromUserModelAsync(UserFormModel userToRegister);

        [RequirePermissions(new[] { AuthorizationPermissions.CanManageUsers })]
        public abstract Task<bool> StoreUserAsync(UserFormModel userToRegist, string password);
    }
}