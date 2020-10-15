using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRIME_UCR.Domain.Models.CheckLists;

namespace PRIME_UCR.Components.CheckLists
{
    public partial class CheckListNavMenu
    {
        [CascadingParameter(Name = "lists")]
        public IEnumerable<CheckList> lists { get; set; }

        [CascadingParameter(Name = "Changelists")]
        public Action<IEnumerable<CheckList>> OnlistsChange { get; set; }

        public bool IsModalOpened { get; set; }

        protected bool collapseNavMenu = true;

        protected string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        protected void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        protected void ToggleIsModalOpened()
        {
            IsModalOpened = !IsModalOpened;
        }

        private void Changelists()
        {
            OnlistsChange?.Invoke(lists);
        }

        protected void Changelists(IEnumerable<CheckList> newlists)
        {
            lists = newlists;
            StateHasChanged();
            Changelists();
        }

        protected void ChangeIsModalOpened(bool newIsModalOpened)
        {
            IsModalOpened = newIsModalOpened;
            StateHasChanged();
        }


        protected string moduleToShow = "Dashboard";

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
                else if (uri == "checklist")
                {
                    showCheckListMenu();
                }
            }
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
