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

        protected IEnumerable<CheckList> lists { get; set; }

        protected IEnumerable<Item> coreItems { get; set; }

        protected IEnumerable<Item> items { get; set; }
        protected IEnumerable<Item> subItems { get; set; }
        protected List<Item> itemsList = new List<Item>();

        protected List<Item> orderedList;
        protected List<int> orderedListLevel;

        public CheckList list { get; set; }

        protected Item tempItem;

        protected bool formInvalid = true;
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
         * Checks if the required fields of the item are valid
         * */
        protected void HandleFieldChanged(object sender, FieldChangedEventArgs e)
        {
            if (tempItem.Nombre != null)
            {
                formInvalid = !editContext.Validate();
            }
        }

        public void Dispose()
        {
            editContext.OnFieldChanged -= HandleFieldChanged;
            createItem = false;
            StateHasChanged();
        }

        /**
         * Inserts a new item into the database
         * */
        protected async Task AddCheckListItem(Item item)
        {
            await MyCheckListService.InsertCheckListItem(item);
            createItem = false;
            formInvalid = true;
            await RefreshModels();
            StateHasChanged();
        }

        /**
         * Sets flags to display the item creation form
         * */
        protected void StartNewItemCreation() 
        {
            createItem = true;
            tempItem = new Item();
            tempItem.IDLista = id;
            tempItem.Orden = coreItems.Count() + 1;
            editContext = new EditContext(tempItem);
            editContext.OnFieldChanged += HandleFieldChanged;
            StateHasChanged();
        }

        /**
         * Sets flags to display the sub item creation form
         * */
        protected async Task CreateSubItem(int itemId)
        {
            subItems = await MyCheckListService.GetItemsBySuperitemId(itemId);
            createItem = true;
            tempItem = new Item();
            tempItem.IDLista = id;
            tempItem.IDSuperItem = itemId;
            tempItem.Orden = subItems.Count() + 1;
            editContext = new EditContext(tempItem);
            editContext.OnFieldChanged += HandleFieldChanged;
            StateHasChanged();
        }

        /**
         * Registers the new item
         * */
        protected async Task HandleValidSubmit()
        {
            await AddCheckListItem(tempItem);
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