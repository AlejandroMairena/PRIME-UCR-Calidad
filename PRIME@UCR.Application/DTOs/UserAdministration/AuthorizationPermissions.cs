using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Application.DTOs.UserAdministration
{
    public class AuthorizationPermissions
    {
        public const string CanDoAnything = "CanDoAnything";
        public const string CanManageUsers = "CanManageUsers";
        public const string CanAccessEverythingExceptMedicalData = "CanAccessEverythingExceptMedicalData";
        public const string CanAccessIncidentsFromAnMedicalRecordInReadMode = "CanAccessIncidentsFromAnMedicalRecordInReadMode";
        public const string CanAccessIncidentsOfHisPatients = "CanAccessIncidentsOfHisPatients";
        public const string CanAssignAllStepsOfAIncidents = "CanAssignAllStepsOfAIncidents";
        public const string CanAssignPostCreationStepsOfIncidentsAssignedToHim = "CanAssignPostCreationStepsOfIncidentsAssignedToHim";
        public const string CanAttachMultimediaInChecklistOfHisPatients = "CanAttachMultimediaInChecklistOfHisPatients";
        public const string CanCreateCheckList = "CanCreateCheckList";
        public const string CanManageAllIncidents = "CanManageAllIncidents";
        public const string CanManageAllMedicalRecords = "CanManageAllMedicalRecords";
        public const string CanManageCheckListOfAnIncidentsAssignedToHim = "CanManageCheckListOfAnIncidentsAssignedToHim";
        public const string CanManageDashboard = "CanManageDashboard";
        public const string CanManageIncidentsAssignedToHim = "CanManageIncidentsAssignedToHim";
        public const string CanManageMedicalRecordsOfHisPatients = "CanManageMedicalRecordsOfHisPatients";
        public const string CanOnlyRegisterAnIncident = "CanOnlyRegisterAnIncident";
        public const string CanSeeAllInfoOfAnIncidentInReadMode = "CanSeeAllInfoOfAnIncidentInReadMode";
        public const string CanSeeMedicalRecordsFromIncidentsInReadMode = "CanSeeMedicalRecordsFromIncidentsInReadMode";
        public const string CanSeeMedicalRecordsInReadMode = "CanSeeMedicalRecordsInReadMode";
        public const string CanSeeMedicalRecordsOfHisPatients = "CanSeeMedicalRecordsOfHisPatients";
        public const string CanSeeMedicalRecordsOfPatientsAssignedToHim = "CanSeeMedicalRecordsOfPatientsAssignedToHim";
    }
}
