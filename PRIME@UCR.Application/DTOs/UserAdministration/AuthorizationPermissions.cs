using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Application.DTOs.UserAdministration
{
    /**     
     * Enumeration used to know each of the policies names to be used in the authorization.
     * The idea is to used in a page a structure like the following to make the authorization:
     * Ej:      @attribute [HasPermission(AuthorizationPermissions.CanManageDashboard)]
     * 
     * Or in a piece of code a structure like the following:
     * Ej:
     * <AuthorizeView Policy="@AuthorizationPermissions.CanManageDashboard">
            <Authorized>
                <h1>
                    Dashboard
                </h1>
            </Authorized>
            <NotAuthorized>
                <h1>
                    No dashboard
                </h1>
            </NotAuthorized>
        </AuthorizeView>
     * In both of them is needed to include the next namespace:
     * using PRIME_UCR.Application.DTOs.UserAdministration;
     * using PRIME_UCR.Application.Implementations.UserAdministration; 
     */

    public enum AuthorizationPermissions
    {
        CanDoAnything = 1,
        CanManageUsers,
        CanAccessEverythingExceptMedicalData,
        CanAccessIncidentsFromAnMedicalRecordInReadMode,
        CanAccessIncidentsOfHisPatients,
        CanAssignAllStepsOfAIncidents,
        CanAssignPostCreationStepsOfIncidentsAssignedToHim,
        CanAttachMultimediaInChecklistOfHisPatients,
        CanCreateCheckList,
        CanManageAllIncidents,
        CanManageAllMedicalRecords,
        CanManageCheckListOfAnIncidentsAssignedToHim,
        CanManageDashboard,
        CanManageIncidentsAssignedToHim,
        CanManageMedicalRecordsOfHisPatients,
        CanOnlyRegisterAnIncident,
        CanSeeAllInfoOfAnIncidentInReadMode,
        CanSeeMedicalRecordsFromIncidentsInReadMode,
        CanSeeMedicalRecordsInReadMode,
        CanSeeMedicalRecordsOfHisPatients,
        CanSeeMedicalRecordsOfPatientsAssignedToHim
    }
}
