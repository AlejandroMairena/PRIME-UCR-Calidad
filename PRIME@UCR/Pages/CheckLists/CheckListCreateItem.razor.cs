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


namespace PRIME_UCR.Pages.CheckLists
{
    public class CheckListCreateItemBase : ComponentBase
    {

        protected IEnumerable<CheckList> lists { get; set; }

        [Inject] protected ICheckListService MyService { get; set; }

        [Inject] public NavigationManager NavManager { get; set; }

        private const string CreateUrl = "/checklist/createItem";
        private string beforeUrl = "/checklist"; //mejorar diseño de interfaz
        private string afterUrl = "";

        protected Item item = new Item();

        protected bool formInvalid = true;
        protected EditContext editContext;

        protected async Task RefreshModels()
        {
            lists = await MyService.GetAll();
        }
        protected override async Task OnInitializedAsync()
        {
            editContext = new EditContext(item);
            editContext.OnFieldChanged += HandleFieldChanged;
            await RefreshModels();
            item.Orden = lists.Count() + 1;
        }

        protected void HandleFieldChanged(object sender, FieldChangedEventArgs e)
        {
            if (item.Nombre != null)
            {
                formInvalid = !editContext.Validate();
            }
            StateHasChanged();
        }


        public void Dispose()
        {
            editContext.OnFieldChanged -= HandleFieldChanged;
        }

        protected async Task AddCheckList(CheckList tempList)
        {
            await MyService.InsertCheckList(tempList);
            await RefreshModels();
        }

        protected async Task AddCheckListItem(Item item)
        {
            await MyService.InsertCheckListItem(item);
            await RefreshModels();
        }


        protected async Task HandleValidSubmit()
        {
            await AddCheckListItem(item);
            afterUrl = "/checklist/";
            NavManager.NavigateTo(afterUrl); // to do: agregar a pagina anterior
        }

    }
}
