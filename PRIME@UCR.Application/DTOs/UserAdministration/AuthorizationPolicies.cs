using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Application.DTOs.UserAdministration
{
    public enum AuthorizationPolicies
    {
        /*User administration policies*/
        CanManageUsers = 0,

        /*Checklist policies*/
        CanCreateCheckList,

        /*Dashboard policies*/
        CanManageDashboard,

        /*Medical records policies*/
        CanManageMedicalRecords,
    }
}
