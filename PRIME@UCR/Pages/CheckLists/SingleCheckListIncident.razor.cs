using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services;
using PRIME_UCR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Application.Services.CheckLists;
using PRIME_UCR.Components.CheckLists;
using Microsoft.AspNetCore.Components.Forms;
using PRIME_UCR.Domain.Models.CheckLists;
using System.Linq;

namespace PRIME_UCR.Pages.CheckLists
{
    public class SingleCheckListIncidentBase : ComponentBase
    {
        public SingleCheckListIncidentBase()
        {
            not_complete = true;
        }
        [Parameter]
        public int id { get; set; }
        [Parameter]
        public int plantillaid { get; set; }
        [Parameter]
        public string incidentcod { get; set; }

        private bool isDisabled { get; set; } = true;//not need

        protected bool createItem { get; set; } = false;// not neet

        public InstanceChecklist insanceLC { get; set; }

        protected List<Item> items { get; set; }// to change
        protected List<InstanciaItem> itemsInstance { get; set; }

        public CheckList list { get; set; }

        protected Item tempItem;//to change

        protected bool formInvalid = true;

        public bool not_complete;

        protected List<InstanciaItem> orderedList;
        protected List<int> orderedListLevel;

        protected int itemIndex { get; set; } = 0;

        [Inject] protected ICheckListService MyCheckListService { get; set; }
        [Inject] protected IInstanceChecklistService MyCheckInstanceChechistService { get; set; }

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
            itemsInstance = tempInstancedItems.ToList();
            IEnumerable<InstanciaItem> coreItems = await MyCheckInstanceChechistService.GetCoreItems(incidentcod, plantillaid);
            orderedList = new List<InstanciaItem>();
            orderedListLevel = new List<int>();
            foreach (var item in coreItems)
            {
                GenerateOrderedList(item, 0);
            }
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
    }
}