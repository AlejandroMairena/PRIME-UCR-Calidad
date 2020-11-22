namespace PRIME_UCR.Domain.Constants
{
    /**
     * Enumeration used to know each of the policies names to be used in the authorization.
     * The idea is to used in a page a structure like the following to make the authorization:
     * Ej:      @attribute [HasPermission(AuthorizationPolicies.CanDoAnything)]
     *
     * Or in a piece of code a structure like the following:
     * Ej:
     * <AuthorizeView Policy="@AuthorizationPolicies.CanDoAnything">
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
        CanManageUsers = 1,
        CanCreateChecklist,
        CanInstantiateChecklist,
        CanSeeIncidentsList,
        CanSeeMedicalInfoInIncidentsList,
        CanSeeBasicDetailsOfIncidents,
        CanSeeMedicalDetailsOfIncidents,
        CanSeeInfoOfIncidentsPatient,
        CanSeeMedicalRecordsOfHisPatients,
        CanSeeAllMedicalRecords,
        CanSeeMedicalInfoOnDashboard,
        CanSeeIncidentsInfoOnDashboard,
        CanCheckItemsInChecklists,
        CanAttachMultimediaInChecklists,
        CanEditBasicDetailsOfIncident,
        CanEditMedicalDetailsOfIncident,
        CanReviewIncidents,
        CanEditMedicalInfoOfIncidentsPatient,
        CanAssignIncidents,
        CanCreateIncidents,
        CanManageIncidentMultimediaContent,
        CanManageIncidentChecklists,
    }
}
