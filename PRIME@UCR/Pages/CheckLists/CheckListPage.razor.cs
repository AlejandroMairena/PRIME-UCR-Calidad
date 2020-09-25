using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services;
using PRIME_UCR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Application.Services.CheckLists;
using PRIME_UCR.Components.CheckLists;
using Microsoft.AspNetCore.Components.Forms;

namespace PRIME_UCR.Pages.CheckLists
{
    public class CheckListPageBase : ComponentBase
    {
        public bool IsModalOpened { get; set; }

        protected IEnumerable<CheckList> lists;

        [Inject] protected ICheckListService MyService { get; set; }

        protected CheckList checkList = new CheckList();

        protected bool formInvalid = true;
        protected EditContext editContext;

        protected async Task RefreshModels()
        {
            lists = await MyService.GetAll();
        }

        protected override async Task OnInitializedAsync()
        {
            editContext = new EditContext(checkList);
            editContext.OnFieldChanged += HandleFieldChanged;
            await RefreshModels();
        }

        protected void HandleFieldChanged(object sender, FieldChangedEventArgs e)
        {
            if (checkList.Nombre != null && checkList.Tipo != null) 
            {
                formInvalid = !editContext.Validate();
            }
            StateHasChanged();
        }

        public void Dispose()
        {
            editContext.OnFieldChanged -= HandleFieldChanged;
        }

        protected void OnClose(string value)
        {
            checkList.Nombre = null;
            checkList.Descripcion = null;
            checkList.Tipo = null;
            checkList.Orden = 0;
            editContext = new EditContext(checkList);
            editContext.OnFieldChanged += HandleFieldChanged;
            formInvalid = true;
        }

        protected async Task AddCheckList(CheckList tempList)
        {
            await MyService.InsertCheckList(tempList);
            await RefreshModels();
        }

        protected async Task HandleValidSubmit()
        {
            await AddCheckList(checkList);
            IsModalOpened = false;
            checkList = new CheckList();
            editContext = new EditContext(checkList);
            editContext.OnFieldChanged += HandleFieldChanged;
            formInvalid = true;
        }
    }
}
