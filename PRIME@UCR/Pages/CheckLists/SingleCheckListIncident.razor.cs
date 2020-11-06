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

        private bool isDisabled { get; set; } = true;

        protected bool createItem { get; set; } = false;

        protected IEnumerable<CheckList> lists { get; set; }

        public InstanceChecklist insanceLC { get; set; }

        protected IEnumerable<InstanceChecklist> instancelists { get; set; }

        protected IEnumerable<Item> items { get; set; }

        public CheckList list { get; set; }

        protected Item tempItem;

        protected bool formInvalid = true;
        protected EditContext editContext;

        public bool not_complete;

        [Inject] protected ICheckListService MyCheckListService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await RefreshModels();
        }

        protected async Task RefreshModels()
        {
            list = await MyCheckListService.GetById(id);
            lists = await MyCheckListService.GetAll();
            items = await MyCheckListService.GetItemsByCheckListId(id);
        }

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

        protected async Task AddCheckListItem(Item item)
        {
            await MyCheckListService.InsertCheckListItem(item);
            createItem = false;
            formInvalid = true;
            await RefreshModels();
            StateHasChanged();
        }

        protected void StartNewItemCreation()
        {
            createItem = true;
            tempItem = new Item();
            tempItem.IDLista = id;
            tempItem.Orden = items.Count() + 1;
            editContext = new EditContext(tempItem);
            editContext.OnFieldChanged += HandleFieldChanged;
            StateHasChanged();
        }

        protected void CreateSubItem(int itemId)
        {
            createItem = true;
            tempItem = new Item();
            tempItem.IDLista = id;
            tempItem.IDSuperItem = itemId;
            // Get the order from ?
            editContext = new EditContext(tempItem);
            editContext.OnFieldChanged += HandleFieldChanged;
            StateHasChanged();
        }

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
    }
}