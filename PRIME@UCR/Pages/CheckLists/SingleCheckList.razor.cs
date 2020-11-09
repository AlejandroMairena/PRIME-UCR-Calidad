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
using System.Threading;
using Microsoft.AspNetCore.Components.Rendering;
using MatBlazor;

namespace PRIME_UCR.Pages.CheckLists
{
    /**
    * This page displays the information of achecklists and its subitems
    * */
    public class SingleCheckListBase : ComponentBase
    {
        [Parameter]
        public int id { get; set; }
        private int Index { get; set; } = 0;

        private bool isDisabled { get; set; } = true;

        protected bool createItem { get; set; } = false;
        protected bool createSubItem { get; set; } = false;
        protected bool editItem { get; set; } = false;

        protected IEnumerable<CheckList> lists { get; set; }

        protected IEnumerable<Item> coreItems { get; set; }

        protected IEnumerable<Item> items { get; set; }
        protected IEnumerable<Item> subItems { get; set; }
        protected List<Item> itemsList = new List<Item>();

        protected List<Item> orderedList;
        protected List<int> orderedListLevel;

        public CheckList list { get; set; }

        protected Item tempItem;
        protected int parentItemId { get; set; }

        protected bool formInvalid = false;
        protected EditContext editContext;

        [Inject] protected ICheckListService MyCheckListService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await RefreshModels();
        }

        /**
         * Gets the checklist corresponding to this page, its items and the list of checklists in the database
         * */
        protected async Task RefreshModels()
        {
            list = await MyCheckListService.GetById(id);
            lists = await MyCheckListService.GetAll();
            items = await MyCheckListService.GetItemsByCheckListId(id);
            itemsList = items.ToList();
            coreItems = await MyCheckListService.GetCoreItems(id);
            orderedList = new List<Item>();
            orderedListLevel = new List<int>();
            foreach (var item in coreItems) {
                GenerateOrderedList(item, 0);
            }
        }

        /**
         * Refreshes de page models and flags when an item creation is completed
         * */
        protected async Task creatingFinished()
        {
            createItem = false;
            createSubItem = false;
            formInvalid = false;
            await RefreshModels();
            StateHasChanged();
        }

        /**
         * Refreshes de page models and flags when an item edition is completed
         * */
        protected async Task editingFinished()
        {
            editItem = false;
            formInvalid = false;
            await RefreshModels();
            StateHasChanged();
        }

        public void Dispose()
        {
            createItem = false;
            createSubItem = false;
            StateHasChanged();
        }


        /**
         * Sets flags to display the item creation form
         * */
        protected void StartNewItemCreation() 
        {
            tempItem = new Item();
            tempItem.IDSuperItem = null;
            tempItem.IDLista = id;
            tempItem.Orden = coreItems.Count() + 1;
            createItem = true;
        }

        /**
         * Sets flags to display the sub item creation form
         * */
        protected async Task CreateSubItem(int itemId)
        {
            subItems = await MyCheckListService.GetItemsBySuperitemId(itemId);
            tempItem = new Item();
            tempItem.IDLista = id;
            tempItem.IDSuperItem = itemId;
            tempItem.Orden = subItems.Count() + 1;
            parentItemId = itemId;
            createSubItem = true;
        }

        protected async Task EditItem(int itemId)
        {
            tempItem = await MyCheckListService.GetItemById(itemId);
            parentItemId = itemId;
            editItem = true;
        }

        protected override async Task OnParametersSetAsync()
        {
            await RefreshModels();
        }

        protected async Task Update()
        {
            await MyCheckListService.UpdateCheckList(list);
            await RefreshModels();
        }

        /**
         * Gets an item based on its id
         * */
        protected int getItemIndex(Item itemInList) {
            return itemsList.FindIndex(item => item.Id == itemInList.Id);
        }

        /**
         * Generates an ordered list of an item's subitems based on its level
         * */
        private void GenerateOrderedList(Item item, int level) 
        {
            orderedList.Add(item);
            orderedListLevel.Add(level);
            List<Item> subItems = itemsList.FindAll(tempItem => tempItem.IDSuperItem == item.Id);
            subItems = subItems.OrderBy(item => item.Orden).ToList<Item>();
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
        protected bool HasSubItems(Item item)
        {
            List<Item> subItems = itemsList.FindAll(tempItem => tempItem.IDSuperItem == item.Id);
            bool hasSubItems = false;
            if (subItems.Count() != 0) 
            {
                hasSubItems = true;
            }
            return hasSubItems;
        }
    }
}