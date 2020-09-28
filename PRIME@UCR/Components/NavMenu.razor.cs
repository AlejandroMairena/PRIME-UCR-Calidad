using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components
{
    public partial class NavMenu
    {
        private bool collapseNavMenu = true;

        private string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        private string moduleToShow = "Dashboard";

        private string classOfModule = "oi oi-dashboard";

        private string showDashboard = "hide";

        private string showMedicalRecords = "show";

        private string showIncidents = "show";

        private string showUserAdmin = "show";

        private string showCheckList = "show";

        protected override void OnInitialized()
        {
            string uri = MyNavegationManager.ToBaseRelativePath(MyNavegationManager.Uri);

            if (uri != "")
            {
                uri = uri.Split('/').First();
                if (uri == "dashboard")
                {
                    showDashboardMenu();
                }
                else if (uri == "incidents")
                {
                    showIncidentsMenu();
                }
                else if (uri == "user_administration")
                {
                    showUserAdminMenu();
                }
                else if (uri == "medical_records")
                {
                    showMedicalRecordsMenu();
                }
                else if (uri == "check_list")
                {
                    showCheckListMenu();
                }
            }
        }

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        private void showIncidentsMenu()
        {
            moduleToShow = "Administración de incidentes de traslado";
            showIncidents = "hide";
            showMedicalRecords = showUserAdmin = showCheckList = showDashboard = "show";
            classOfModule = "oi oi-map";
        }

        private void showCheckListMenu()
        {
            moduleToShow = "Plantillas de listas de chequeo";
            showCheckList = "hide";
            showMedicalRecords = showUserAdmin = showIncidents = showDashboard = "show";
            classOfModule = "oi oi-list";
        }

        private void showMedicalRecordsMenu()
        {
            moduleToShow = "Expedientes";
            showMedicalRecords = "hide";
            showCheckList = showUserAdmin = showIncidents = showDashboard = "show";
            classOfModule = "oi oi-list";
        }

        private void showDashboardMenu()
        {
            moduleToShow = "Dashboard";
            showDashboard = "hide";
            showCheckList = showUserAdmin = showIncidents = showMedicalRecords = "show";
            classOfModule = "oi oi-dashboard";
        }

        private void showUserAdminMenu()
        {
            moduleToShow = "Administración de usuarios y perfiles";
            showUserAdmin = "hide";
            showCheckList = showDashboard = showIncidents = showMedicalRecords = "show";
            classOfModule = "oi oi-book";
        }
    }
}
