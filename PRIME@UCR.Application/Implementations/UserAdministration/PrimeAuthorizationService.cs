using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Application.Implementations
{
    /**
     * Class used to manage the authorization of the users. 
     */
    public class PrimeAuthorizationService : IPrimeAuthorizationService
    {
        /**
         * Method used to handle the authorization of an user to the aplication.
         */
        public bool HavePolicy(int policy, List<Permiso> permissionsList)
        {
            switch (policy)
            {
                case (int)AuthorizationPolicies.CanManageUsers:
                    return CanManageUsers(permissionsList);
                case (int)AuthorizationPolicies.CanCreateCheckList:
                    return CanCreateChecklist(permissionsList);
                case (int)AuthorizationPolicies.CanManageDashboard:
                    return CanManageDashboard(permissionsList);
                case (int)AuthorizationPolicies.CanManageMedicalRecords:
                    return CanAccessMedicalRecords(permissionsList);
                default:
                    return false;
            }
        }

        private bool CanManageUsers(List<Permiso> permissionsList)
        {
            return permissionsList.Exists(p => p.IDPermiso == (int)AuthorizationPermissions.CanDoAnything)
                    || permissionsList.Exists(p => p.IDPermiso == (int)AuthorizationPermissions.CanManageUsers);
        }

        private bool CanCreateChecklist(List<Permiso> permissionsList)
        {
            return permissionsList.Exists(p => p.IDPermiso == (int)AuthorizationPermissions.CanCreateCheckList)
                    || permissionsList.Exists(p => p.IDPermiso == (int)AuthorizationPermissions.CanDoAnything);
        }

        private bool CanManageDashboard(List<Permiso> permissionsList)
        {
            return permissionsList.Exists(p => p.IDPermiso == (int)AuthorizationPermissions.CanManageDashboard)
                    || permissionsList.Exists(p => p.IDPermiso == (int)AuthorizationPermissions.CanDoAnything);
        }

        private bool CanAccessMedicalRecords(List<Permiso> permissionsList)
        {
            return permissionsList.Exists(p => p.IDPermiso == (int)AuthorizationPermissions.CanManageAllMedicalRecords)
                    || permissionsList.Exists(p => p.IDPermiso == (int)AuthorizationPermissions.CanDoAnything)
                    || (permissionsList.Exists(p => p.IDPermiso == (int)AuthorizationPermissions.CanManageMedicalRecordsOfHisPatients) 
                        /*Falta revisar si es paciente pertenece a su lista de pacientes*/)
                    || permissionsList.Exists(p => p.IDPermiso == (int)AuthorizationPermissions.CanSeeMedicalRecordsInReadMode)
                    || (permissionsList.Exists(p => p.IDPermiso == (int)AuthorizationPermissions.CanSeeMedicalRecordsOfHisPatients)
                        /*Falta revisar si es paciente pertenece a su lista de pacientes*/);
        }
    }
}
