using Microsoft.AspNetCore.Authorization;
using PRIME_UCR.Application.DTOs.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Application.Implementations.UserAdministration
{
    /*
    * Class used to handle the authorization of pages.
    */
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    public class HasPolicyAttribute : AuthorizeAttribute
    {
        public HasPolicyAttribute(AuthorizationPolicies policy) : base(policy.ToString())
        {

        }
    }
}
