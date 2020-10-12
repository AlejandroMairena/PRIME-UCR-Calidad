using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Services.UserAdministration
{
    public interface IPrimeAuthorizationService
    {
        /*Admin*/
        string CanDoAnything();
        string CanManageUsers();

        /*Medical technician specialist*/
        string CanManageIncidentsAssignedToHim();
        string CanManageCheckListOfAnIncidentsAssignedToHim();
        string CanSeeMedicalRecordsOfPatientsAssignedToHim();
        string CanAssignPostCreationStepsOfIncidentsAssignedToHim();
        string CanSeeMedicalRecordsInReadMode();

        /*Doctor*/
        string CanManageMedicalRecordsOfHisPatients();
        string CanAccessIncidentsOfHisPatients();
        string CanAttachMultimediaInChecklistOfHisPatients();
        string CanSeeMedicalRecordsOfHisPatients();

        /*Medical manager*/
        string CanManageAllMedicalRecords();
        string CanAccessIncidentsFromAnMedicalRecordInReadMode();
        string CanManageDashboard();

        /*Medical technician coordinator*/
        string CanCreateCheckList();
        string CanManageAllIncidents();
        string CanAssignAllStepsOfAIncidents();
        string CanSeeMedicalRecordsFromIncidentsInReadMode();

        /*Admin of the control center*/
        string CanSeeAllInfoOfAnIncidentInReadMode();
        string CanOnlyRegisterAnIncident();
        string CanAccessEverythingExceptMedicalData();
    }
}
