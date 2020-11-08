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
using Radzen;

namespace PRIME_UCR.Components.CheckLists
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
        protected IEnumerable<InstanceChecklist> instancelists { get; set; }
        
        public List<CheckList> TempInstance = new List<CheckList>();
        public List<Todo> TempDetail = new List<Todo>();
        public List<int> TempsIds = new List<int>();
        
        public bool llenado = false;
        public bool dont_save = true;
        public int min = 0;//sumar 1
        public int count = 0;
        public string afterUrl;

        /*
         * asig incident code
         
        public void AsignIncidentCode(string cod)
        {
            incidentCod = cod;
        }*/
        protected override async Task OnInitializedAsync()
        {
            await RefreshModels();
        }
        /**
         * Gets the list of checklists and list of instance checklists in the database
         * */
        protected async Task RefreshModels()
        {
            lists = await MyService.GetAll();
            instancelists = await MyInstanceChecklistService.GetAll();
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
            //await Update();
            OnInitialized();
            count = min;
            dont_save = true;
        }

        public void CancelAsignment() {
            Dispose();
            NavManager.NavigateTo($"/incidents/{incidentCod}");
        }

        protected void CheckIempList(int idd, ChangeEventArgs e)
        {
            TempDetail[TempsIds.IndexOf(idd)].IsDone = (bool)e.Value;
            count += (bool)e.Value ? 1 : -1;
            update_save();
        }

        public void update_save()
        {
            dont_save = (count > 0) ? false : true;
        }

        protected void upate_Count()
        {
            int mycount = 0;
            foreach (var temp in TempDetail)
            {
                if (temp.IsDone == true)
                {
                    ++mycount;
                }
            }
            count = mycount;
            Update();
            update_save();
        }

        /*
         * rellena el arreglo de la cantidad de listas de chequeo en falso
         */
        public void Llenar()
        {
            if (llenado == false)
            {
                foreach (var templist in lists)
                {
                    Todo TodoItem = new Todo();
                    int idds = @templist.Id;
                    TodoItem.idd = idds;
                    TempDetail.Add(TodoItem);
                    TempsIds.Add(idds);
                }
                llenado = true;
            }
        }

        /**
         * Inserts the new instance checklist into the database
         * */
        protected async Task AddInstanceCheckList(InstanceChecklist tempList)
        {
            await MyInstanceChecklistService.InsertInstanceChecklist(tempList);
            await RefreshModels();
        }

        /**
        * Inserts the new instance checklist into the database
        * */
        protected async Task AddAsign()
        {
            InstanceChecklist instance = new InstanceChecklist();
            int countlist = min;
            foreach (var tempList in TempDetail)
            {
                if (tempList.IsDone)
                {
                    ++countlist;
                    //instance.InstanciadoId = count;//to review
                    instance.IncidentCod = incidentCod;
                    instance.PlantillaId = tempList.idd;
                    await MyInstanceChecklistService.InsertInstanceChecklist(instance);
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
            NavManager.NavigateTo($"/incidents/{incidentCod}");
        }
    }
}