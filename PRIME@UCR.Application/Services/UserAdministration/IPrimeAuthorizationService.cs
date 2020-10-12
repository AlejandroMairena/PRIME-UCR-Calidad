using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Services.UserAdministration
{
    /*
     * Interface used to manage all the policies of the autorization.
     */
    public interface IPrimeAuthorizationService
    {
        /*Admin permissions*/
        string CanDoAnything();
        string CanManageUsers();

        /*Medical technician specialist permissions*/
        string CanManageIncidentsAssignedToHim();
        string CanManageCheckListOfAnIncidentsAssignedToHim();
        string CanSeeMedicalRecordsOfPatientsAssignedToHim();
        string CanAssignPostCreationStepsOfIncidentsAssignedToHim();
        string CanSeeMedicalRecordsInReadMode();

        /*Doctor permissions*/
        string CanManageMedicalRecordsOfHisPatients();
        string CanAccessIncidentsOfHisPatients();
        string CanAttachMultimediaInChecklistOfHisPatients();
        string CanSeeMedicalRecordsOfHisPatients();

        /*Medical manager permissions*/
        string CanManageAllMedicalRecords();
        string CanAccessIncidentsFromAnMedicalRecordInReadMode();
        string CanManageDashboard();

        /*Medical technician coordinator permissions*/
        string CanCreateCheckList();
        string CanManageAllIncidents();
        string CanAssignAllStepsOfAIncidents();
        string CanSeeMedicalRecordsFromIncidentsInReadMode();

        /*Admin of the control center permissions*/
        string CanSeeAllInfoOfAnIncidentInReadMode();
        string CanOnlyRegisterAnIncident();
        string CanAccessEverythingExceptMedicalData();
    }
}
