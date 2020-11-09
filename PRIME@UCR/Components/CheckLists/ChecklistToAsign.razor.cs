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

namespace PRIME_UCR.Components.CheckLists
{
    public class ChecklistToAsignBase : ComponentBase
    {

        public ChecklistToAsignBase()
        {
            llenado = false;
            count = min;
            dont_save = true;
        }
        protected IEnumerable<CheckList> lists { get; set; }
        protected IEnumerable<InstanceChecklist> instancelists { get; set; }

        protected InstanceChecklist instanceCL = new InstanceChecklist();
        public bool llenado;
        [Inject] protected ICheckListService MyService { get; set; }
        [Inject] protected IInstanceChecklistService MyInstanceChecklistService { get; set; }
        [Inject] public NavigationManager NavManager { get; set; }

        public List<CheckList> TempInstance = new List<CheckList>();
        public List<Todo> TempDetail = new List<Todo>();
        public List<int> TempsIds = new List<int>();
        public int count;
        public bool dont_save;
        public int min = 0;//sumar 1
        public string afterUrl;
        [Parameter] public string incidentCod { get; set; }
        /*
         * asig incident code
         */
        public void AsignIncidentCode(string cod)
        {
            incidentCod = cod;
        }
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
            await MyService.UpdateCheckList((CheckList)lists);
            await RefreshModels();
        }
        public void Dispose()
        {
            foreach (var temp in TempDetail)
            {
                temp.IsDone = false;
            }
            Update();
            OnInitialized();
            count = min;
            dont_save = true;

        }
        protected void CheckIempList(int idd, ChangeEventArgs e)
        {
            if ((bool)e.Value)
            {
                TempDetail[TempsIds.IndexOf(idd)].IsDone = true;
                count++;
                update_save();
            }
            else
            {
                TempDetail[TempsIds.IndexOf(idd)].IsDone = false;
                count--;
                update_save();
            }
        }

        public void update_save()
        {
            if (count > 0)
            {
                dont_save = false;
            }
            else
            {
                dont_save = true;
            }
        }

        protected void upate_Count()
        {
            int mycount = 0;
            foreach (var temp in TempDetail)
            {
                if (temp.IsDone == true)
                {
                    mycount++;
                }
            }
            count = mycount;
            Update();
            update_save();
        }

        protected void toggleItemChangeComponent()
        {
        }
        public class Todo
        {
            public bool IsDone { get; set; }
            public int idd;
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
        /*
         * Checks if the required fields of the checklist are valid
         * */
        protected void HandleFieldChanged(object sender, FieldChangedEventArgs e)
        {

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
            foreach ( var tempList in TempDetail)
            {
                if (tempList.IsDone)
                {
                    countlist++;
                    instance.InstanciadoId = count;//to review
                    instance.IncidentCod = incidentCod;
                    instance.PlantillaId = tempList.idd;
                    await MyInstanceChecklistService.InsertInstanceChecklist(instance);
                }
            }
            RefreshModels();
            
        }

        /**
         * Registers the new checklist and navigates to its page
         * */
        protected async Task HandleValidSubmit()
        {
            //instanceCL.InstanciadoId = 2;
            //await AddInstanceCheckList(instanceCL);
            await AddAsign();
            afterUrl = "/checklist/" + "1";// instanceCL.InstanciadoId;
            NavManager.NavigateTo(afterUrl); // to do: agregar a pagina anterior
        }
    }
}