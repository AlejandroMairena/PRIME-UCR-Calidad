using PRIME_UCR.Application.Services.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Application.Implementations
{
    /**
     * Class used to manage the authorization of the users. 
     * At this moment all methods return true. Once the sprint finish all of them would made the queries necessaries to get the correct result and would be documented.
     */
    public class PrimeAuthorizationService : IPrimeAuthorizationService
    {
        public string CanAccessEverythingExceptMedicalData()
        {
            return "true";
        }

        public string CanAccessIncidentsFromAnMedicalRecordInReadMode()
        {
            return "true";
        }

        public string CanAccessIncidentsOfHisPatients()
        {
            return "true";
        }

        public string CanAssignAllStepsOfAIncidents()
        {
            return "true";
        }

        public string CanAssignPostCreationStepsOfIncidentsAssignedToHim()
        {
            return "true";
        }

        public string CanAttachMultimediaInChecklistOfHisPatients()
        {
            return "true";
        }

        public string CanCreateCheckList()
        {
            return "true";
        }

        public string CanDoAnything()
        {
            return "true";
        }

        public string CanManageAllIncidents()
        {
            return "true";
        }

        public string CanManageAllMedicalRecords()
        {
            return "true";
        }

        public string CanManageCheckListOfAnIncidentsAssignedToHim()
        {
            return "true";
        }

        public string CanManageDashboard()
        {
            return "false";
        }

        public string CanManageIncidentsAssignedToHim()
        {
            return "true";
        }

        public string CanManageMedicalRecordsOfHisPatients()
        {
            return "true";
        }

        public string CanManageUsers()
        {
            return "true";
        }

        public string CanOnlyRegisterAnIncident()
        {
            return "true";
        }

        public string CanSeeAllInfoOfAnIncidentInReadMode()
        {
            return "true";
        }

        public string CanSeeMedicalRecordsFromIncidentsInReadMode()
        {
            return "true";
        }

        public string CanSeeMedicalRecordsInReadMode()
        {
            return "true";
        }

        public string CanSeeMedicalRecordsOfHisPatients()
        {
            return "true";
        }

        public string CanSeeMedicalRecordsOfPatientsAssignedToHim()
        {
            return "true";
        }
    }
}
