using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services;
using PRIME_UCR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Application.Services.CheckLists;
using PRIME_UCR.Components.CheckLists;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Components.Incidents.IncidentDetails.Constants;
using Microsoft.AspNetCore.Components.Forms;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Domain.Models.CheckLists;
using PRIME_UCR.Domain.Models.Incidents;
using System;
using System.Linq;
using MatBlazor;
using PRIME_UCR.Application.Services.Multimedia;

namespace PRIME_UCR.Pages.CheckLists
{
    public class SingleCheckListIncidentBase : ComponentBase
    {
        public SingleCheckListIncidentBase()
        {
            not_complete = true;
            validateEdit = false;
            stateInstanceList= "Pendiente";

         }
    [Parameter]
        public int id { get; set; }
        [Parameter]
        public int plantillaid { get; set; }
        [Parameter]
        public string incidentcod { get; set; }
        public Estado state { get; set; }

        public bool validateEdit;
        public string stateInstanceList;
        private bool isDisabled { get; set; } = true;//not need

        protected bool createItem { get; set; } = false;// not neet
        protected IEnumerable<InstanciaItem> coreItems { get; set; }

        public InstanceChecklist insanceLC { get; set; }

        protected List<Item> items { get; set; }// to change
        protected List<InstanciaItem> itemsInstance { get; set; }

        public CheckList list { get; set; }

        protected Item tempItem;//to change

        protected bool formInvalid = true;

        public bool not_complete;

        protected List<InstanciaItem> orderedList;
        protected List<int> orderedListLevel;
        protected string IncidentURL = "/incidents/";

        protected int itemIndex { get; set; } = 0;

        protected List<List<MultimediaContent>> MyMultimediaContent { get; set; }
        protected bool _isLoading { get; set; } = false;

        [Inject] protected ICheckListService MyCheckListService { get; set; }
        [Inject] protected IInstanceChecklistService MyCheckInstanceChechistService { get; set; }
        [Inject] protected IIncidentService MyIncidentService { get; set; }
        [Inject] private NavigationManager NavManager { get; set; }
        [Inject] protected IMultimediaContentService MyMultimediaContentService { get; set; }
        //detals for state incident
        [Parameter] public IncidentDetailsModel Incident { get; set; }

        // Info for Incident summary that is shown at top of the page
        public IncidentSummary Summary = new IncidentSummary();
        protected override async Task OnInitializedAsync()
        {
            await RefreshModels();
        }

        protected async Task RefreshModels()
        {
            list = await MyCheckListService.GetById(plantillaid);
            IEnumerable<Item> tempItems = await MyCheckListService.GetItemsByCheckListId(plantillaid);
            items = tempItems.ToList();
            IEnumerable<InstanciaItem> tempInstancedItems = await MyCheckInstanceChechistService.GetItemsByIncidentCodAndCheckListId(incidentcod, plantillaid);
            MyMultimediaContent = new List<List<MultimediaContent>>();
            // _isLoading = new List<bool>();
            foreach (var item in tempInstancedItems)
            {
                List<MultimediaContent> tempList = (await MyMultimediaContentService.GetByCheckListItem(item.ItemId, item.PlantillaId, item.IncidentCod)).ToList();
                MyMultimediaContent.Add(tempList);
                // _isLoading.Add(false);
            }
            itemsInstance = tempInstancedItems.ToList();
            coreItems = await MyCheckInstanceChechistService.GetCoreItems(incidentcod, plantillaid);
            orderedList = new List<InstanciaItem>();
            orderedListLevel = new List<int>();
            foreach (var item in coreItems)
            {
                GenerateOrderedList(item, 0);
            }
            state = await MyIncidentService.GetIncidentStateByIdAsync(incidentcod);
            Incident = await MyIncidentService.GetIncidentDetailsAsync(incidentcod);
            Summary.LoadValues(Incident);
            updateState();
        }

        protected override async Task OnParametersSetAsync()
        {
            await RefreshModels();
        }
        protected async Task Update()
        {
            await MyCheckInstanceChechistService.UpdateInstanceChecklist(insanceLC);
            await RefreshModels();

        }
        /*
         * ALL states if incident
             ('En proceso de creación'),
            ('Creado'),
            ('Rechazado'),
            ('Aceptado'),
            ('Asignado'),
            ('En preparación'),
            ('En ruta a origen'),
            ('Paciente recolectado en origen'),
            ('En traslado'),
            ('Entregado'),
            ('Reactivación'),
            ('Finalizado')
        */
        public void updateState()
        {
            if (state.Nombre == "Asignado" || state.Nombre == "En preparación" || state.Nombre == "En ruta a origen" || state.Nombre == "Paciente recolectado en origen" || state.Nombre == "En traslado" || state.Nombre ==  "Entregado" || state.Nombre == "Reactivación")
            {
                validateEdit = true;
            }
            else
            { //the state is: "En proceso de creación" || "Creado" || "Rechazdo" || "Aceptado" || "Finalizado" 
                validateEdit = false;
            }
        }
        public async Task GetName(int id)
        {

            list = await MyCheckListService.GetById(id);

        }
        public async Task GetTipo(int id)
        {

            list = await MyCheckListService.GetById(id);

        }
        public async Task GetDescp(int id)
        {

            list = await MyCheckListService.GetById(id);


        }
        public string GetName2(int id)
        {
            GetName(id);
            return list.Nombre;
        }

        public string GetTipo2(int id)
        {
            GetTipo(id);
            return list.Tipo;
        }
        public string GetDescp2(int id)
        {
            GetDescp(id);
            return list.Descripcion;
        }
        public bool Getincident()
        {
            //consultar incidente
            //to change
            return true;
        }

        protected int GetItemIndex(InstanciaItem instance)
        {
            itemIndex = items.FindIndex(a => a.Id == instance.ItemId);
            return itemIndex;
        }

        private void GenerateOrderedList(InstanciaItem item, int level)
        {
            orderedList.Add(item);
            orderedListLevel.Add(level);
            List<InstanciaItem> subItems = itemsInstance.FindAll(tempItem => tempItem.ItemPadreId == item.ItemId);
            if (subItems.Count() > 0)
            {
                foreach (var tempSubtem in subItems)
                {
                    GenerateOrderedList(tempSubtem, level + 1);
                }
            }
        }

        /**
         * Checks if an item has subitems
         * */
        protected bool HasSubItems(InstanciaItem item)
        {
            List<InstanciaItem> subItems = itemsInstance.FindAll(tempItem => tempItem.ItemPadreId == item.ItemId);
            bool hasSubItems = false;
            if (subItems.Count() != 0)
            {
                hasSubItems = true;
            }
            return hasSubItems;
        }

        protected string truncate(string text, int level, int lines)
        {
            if (String.IsNullOrEmpty(text)) return "";
            int maxLength = lines * (65 - level * 5);
            return text.Length <= maxLength ? text : text.Substring(0, maxLength) + "...";
        }

        protected MatTheme AddButtonTheme = new MatTheme()
        {
            Primary = "white",
            Secondary = "#095290"
        };

        protected void Redirect()
        {
            IncidentURL += incidentcod;
            IncidentURL += "/Checklist";
            NavManager.NavigateTo($"{IncidentURL}");
        }

        protected void CheckItem(InstanciaItem itemIn, ChangeEventArgs e)
        {
             itemIn.Completado = (bool)e.Value;
            // metodo //MyCheckInstanceChechistService.UpdateItem(itemIn);
            //count += (bool)e.Value ? 1 : -1;
            getDate(itemIn, e);
            
            MyCheckInstanceChechistService.UpdateItemInstance(itemIn);
            RefreshModels();
        }

        ///Borrar
        // protected async Task updateItemInstance(InstanciaItem item) {
        //     await MyCheckInstanceChechistService.UpdateItemInstance(item);
        //     await RefreshModels();
        // }
        //

        //metodo para actualizar estado de la lista de cheuqueo 
        //unpdate
        // {"Pendiente","En progreso","Completada"};

        protected async Task OnFileUpload(InstanciaItem item, MultimediaContent mc)
        {
            //var i = _actionTypes.IndexOf(action);
            //await MultimediaContentService.AddMultContToAction(Incident.AppointmentId, action.Nombre, mc.Id);
            //_existingFiles[i].Add(mc);
            _isLoading = true;
            StateHasChanged();

            MultimediaContentItem MyMultimedia = new MultimediaContentItem
            {
                Id_MultCont = mc.Id, Id_Item = item.ItemId, Id_Lista = item.PlantillaId, Codigo_Incidente = item.IncidentCod
            };
            await MyMultimediaContentService.AddMultContToCheckListItem(MyMultimedia);
            int index = itemsInstance.FindIndex(i => i.ItemId == item.ItemId);
            MyMultimediaContent[index].Add(mc);
            _isLoading = false;
        }
        protected void getDate(InstanciaItem Item, ChangeEventArgs e)
        {
            if ((bool)e.Value == true)
            {
                Item.FechaHoraInicio = DateTime.Now;
            }
            else {
                Item.FechaHoraInicio = null;
            }

           RefreshModels();
            
        }
    }
}