using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Application.DTOs.UserAdministration
{
    /**     
     * Enumeration used to know each of the policies names to be used in the authorization.
     * The idea is to used in a page a structure like the following to make the authorization:
     * Ej:      @attribute [HasPolicy(AuthorizationPolicies.CanManageDashboard)]
     * 
     * Or in a piece of code a structure like the following:
     * Ej:
     * <AuthorizeView Policy="@AuthorizationPolicies.CanManageDashboard">
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

        CanManageIncidentsAssignedToHim,
        CanManageCheckListOfAnIncidentsAssignedToHim,
        CanSeeMedicalRecordsOfPatientsAssignedToHim,
        CanAssignPostCreationStepsOfIncidentsAssignedToHim,
        CanSeeMedicalRecordsInReadMode,

        CanManageMedicalRecordsOfHisPatients,
        CanAccessIncidentsOfHisPatients,
        CanAttachMultimediaInChecklistOfHisPatients,
        CanSeeMedicalRecordsOfHisPatients,

        CanManageAllMedicalRecords,
        CanAccessIncidentsFromAnMedicalRecordInReadMode,
        CanManageDashboard,

        CanCreateCheckList,
        CanManageAllIncidents,
        CanAssignAllStepsOfAIncidents,
        CanSeeMedicalRecordsFromIncidentsInReadMode,

        CanSeeAllInfoOfAnIncidentExceptMultimedia,
        CanOnlyRegisterAnIncident,
        CanAccessEverythingExceptMedicalData
    }
}
