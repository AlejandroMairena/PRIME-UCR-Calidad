using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services;
using PRIME_UCR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Application.Services.CheckLists;
using PRIME_UCR.Components.CheckLists;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq;
using PRIME_UCR.Domain.Models.CheckLists;
using MatBlazor;

namespace PRIME_UCR.Components.CheckLists
{
    /**
    * This page displays every checklist instance in especific incident and general data for each one
    * */
    public class AllChecklistSingleIncidentBase : ComponentBase
    {

        [Inject]
        private NavigationManager NavManager { get; set; }

        protected IEnumerable<CheckList> lists { get; set; }
        protected IEnumerable<InstanceChecklist> instancelists { get; set; }
        protected List<InstanceChecklist> instancelistsThis { get; set; }
        [Inject] protected ICheckListService MyCheckListService { get; set; }
        [Inject] protected IInstanceChecklistService MyInstanceService { get; set; }
        [Parameter] public string IncidentCod { get; set; }
        public CheckList Alist = new CheckList(); //templist
        protected List<string> SummaryList { get; set; }
        /* DIOSVIER
         * Se debe hacer Inject de IIncidentService y usar el metodo: GetIncidentStateByIdAsync(Incident id).
         * Tiene un atributo Nombre, entonce un ej de uso: var state = incidentService.GetIncidentStateByIdAsync(code) -> state.Nombre es el que tiene el estado
         */
        protected override async Task OnInitializedAsync()
        {
            SummaryList = new List<string>();
            await RefreshModels();
        }

        /**
         * Gets the list of checklists in the database
         * */
        protected async Task RefreshModels()
        {
            lists = await MyCheckListService.GetAll();
            instancelists = await MyInstanceService.GetByIncidentCod(IncidentCod);
            foreach(var tempList in instancelists)
            {
                await GetSummary(tempList.PlantillaId);
            }
        }
        public async Task GetName (int id)
        {

            Alist = await MyCheckListService.GetById(id);
            
        }
        public async Task GetTipo(int id)
        {

            Alist = await MyCheckListService.GetById(id);
            
        }
        public async Task GetDescp(int id)
        {

            Alist = await MyCheckListService.GetById(id);
            
        }
        public string GetName2(int id)
        {
            GetName(id);
            return Alist.Nombre;
        }

        public string GetTipo2(int id)
        {
            GetTipo(id);
            return Alist.Tipo;
        }
        public string GetDescp2(int id)
        {
            GetDescp(id);
            return Alist.Descripcion;
        }

        public async Task GetSummary(int checkListId)
        {
            int totalItems = await MyInstanceService.GetNumberOfItems(IncidentCod, checkListId);
            int completedItems = await MyInstanceService.GetNumberOfCompletedItems(IncidentCod, checkListId);
            string summary = completedItems + "/" + totalItems;
            SummaryList.Add(summary);
        }

        public string GetSpecificSummary(int checkListId)
        {
            List<InstanceChecklist> tempList = instancelists.ToList();
            int index = tempList.FindIndex(a => a.PlantillaId == checkListId);
            return SummaryList[index];
        }
    }
}
