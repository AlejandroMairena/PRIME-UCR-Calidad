using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services;
using PRIME_UCR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Application.Services.CheckLists;
using PRIME_UCR.Components.CheckLists.InIncident;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq;
using PRIME_UCR.Domain.Models.CheckLists;
using Radzen;

namespace PRIME_UCR.Components.CheckLists.InIncident
{
    public class ChecklistToAsignBase : ComponentBase
    {
        public class Todo
        {
            public bool IsDone { get; set; }
            public int idd;
        }

        [Inject] protected ICheckListService MyService { get; set; }
        [Inject] protected IInstanceChecklistService MyInstanceChecklistService { get; set; }
        [Inject] public NavigationManager NavManager { get; set; }

        [Parameter] public string incidentCod { get; set; }

        protected IEnumerable<CheckList> lists { get; set; }
        protected List<CheckList> TempLists { get; set; }
        protected IEnumerable<InstanceChecklist> instancelists { get; set; }

        public List<CheckList> TempInstance = new List<CheckList>();
        public List<Todo> TempDetail = new List<Todo>();
        public List<int> TempsIds = new List<int>();

        public bool dont_save = true;
        public int min = 0;//sumar 1
        public int count = 0;
        public string afterUrl;

        protected override async Task OnInitializedAsync()
        {
            await RefreshModels();
            Llenar();
        }
        /**
         * Gets the list of checklists and list of instance checklists in the database
         * */
        protected async Task RefreshModels()
        {
            TempLists = new List<CheckList>();
            lists = await MyService.GetAll();
            instancelists = await MyInstanceChecklistService.GetByIncidentCod(incidentCod);
        }

        protected async Task Update()
        {
            foreach (CheckList list in lists)
            {
                await MyService.UpdateCheckList(list);
            }
            await RefreshModels();
        }

        public void Dispose()
        {
            foreach (var temp in TempDetail)
            {
                temp.IsDone = false;
            }
            OnInitialized();
            count = min;
            dont_save = true;
        }

        public void CancelAsignment() {
            Dispose();
            NavManager.NavigateTo($"/incidents/{incidentCod}/Checklist");
        }

        protected void CheckIempList(int idd, CheckList list, ChangeEventArgs e)
        {
            TempDetail[TempsIds.IndexOf(idd)].IsDone = (bool)e.Value;
            count += (bool)e.Value ? 1 : -1;
            if ((bool)e.Value == true)
            {
                TempLists.Add(list);
            }
            else 
            {
                TempLists.Remove(list);
            }
            update_save(idd, (bool)e.Value);
            StateHasChanged();
        }

        public void update_save(int idd, bool asign)
        {
            if (asign && instancelists.Any(p => p.PlantillaId == idd && p.IncidentCod == incidentCod))
            {
                dont_save = true;
            }
            else
            {
                dont_save = false;
            }
        }

        /*
         * rellena el arreglo de la cantidad de listas de chequeo en falso
         */
        public void Llenar()
        {
            foreach (var templist in lists)
            {
                Todo TodoItem = new Todo();
                int idds = @templist.Id;
                TodoItem.idd = idds;
                if (instancelists.Any(p => p.PlantillaId == idds && p.IncidentCod == incidentCod))
                {
                    TodoItem.IsDone = true;
                    TempLists.Add(templist);
                }
                TempDetail.Add(TodoItem);
                TempsIds.Add(idds);
            }
        }

        /**
         * Inserts the new instance checklist into the database
         * */
        protected async Task AddInstanceCheckList(InstanceChecklist instance)
        {
            if (!instancelists.Any(p => p.PlantillaId == instance.PlantillaId && p.IncidentCod == incidentCod))
            {
                await MyInstanceChecklistService.InsertInstanceChecklist(instance);
                await AddCoreItems(instance);
            }
        }

        /**
         * Starts the recursive method to insert all items
         * */
        protected async Task AddCoreItems(InstanceChecklist instance)
        {
            IEnumerable<Item> coreItems = await MyService.GetCoreItems(instance.PlantillaId);
            foreach (var item in coreItems)
            {
                await AddAllItems(item, instance.PlantillaId, null);
            }
        }

        /**
         * Recursive method that inserts all items and gives them all their attributes.
         * */
        protected async Task AddAllItems(Item item, int checklistId, int? parentItemId)
        {
            InstanciaItem itemInstance = new InstanciaItem();
            itemInstance.ItemId = item.Id;
            itemInstance.PlantillaId = checklistId;
            itemInstance.IncidentCod = incidentCod;
            itemInstance.Completado = false;
            if (parentItemId != null)
            {
                itemInstance.ItemPadreId = parentItemId;
                itemInstance.PlantillaPadreId = checklistId;
                itemInstance.IncidentCodPadre = incidentCod;
            }
            IEnumerable<Item> subItems = await MyService.GetItemsBySuperitemId(item.Id);
            if (subItems.Count() == 0)
            {
                await MyInstanceChecklistService.InsertInstanceItem(itemInstance);
            }
            else
            {
                await MyInstanceChecklistService.InsertInstanceItem(itemInstance);
                foreach (var subItem in subItems) 
                {
                    await AddAllItems(subItem, checklistId, item.Id);
                }
            }
        }

        protected async Task DeleteInstanceCheckList(InstanceChecklist instance)
        {
            if (instancelists.Any(p => p.PlantillaId == instance.PlantillaId && p.IncidentCod == incidentCod))
            {
                await MyInstanceChecklistService.DeleteInstanceChecklist(instance.PlantillaId, instance.IncidentCod);
            }
        }

        /**
        * Inserts the new instance checklist into the database
        * */
        protected async Task AddAsign()
        {
            int countlist = min;
            foreach (var tempList in TempDetail)
            {
                InstanceChecklist instance = new InstanceChecklist();
                instance.IncidentCod = incidentCod;
                instance.PlantillaId = tempList.idd;
                if (tempList.IsDone)
                {
                    ++countlist;
                    await AddInstanceCheckList(instance);
                }
                else
                {
                    await DeleteInstanceCheckList(instance);
                }
            }
            await RefreshModels();
        }

        /**
         * 
         * */
        protected async Task HandleSubmit()
        {
            await AddAsign();
            //afterUrl = "/incidents/" + "1";// instanceCL.InstanciadoId;
            //NavManager.NavigateTo(afterUrl); // to do: go to checklist panel
            NavManager.NavigateTo($"/incidents/{incidentCod}/Checklist");
        }
    }
}