using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services;
using PRIME_UCR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Application.Services.CheckLists;
using PRIME_UCR.Components.CheckLists;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq;
using System;

namespace PRIME_UCR.Components.CheckLists
{
    public class CreateChecklistPopupBase: ComponentBase
    {
        [CascadingParameter(Name = "IsModalOpened")]
        public bool IsModalOpened { get; set; }

        [Parameter]
        public IEnumerable<CheckList> lists { get; set; }

        [Parameter]
        public Action<IEnumerable<CheckList>> OnlistsChange { get; set; }

        [Parameter]
        public Action<bool> OnIsModalOpenedChange { get; set; }

        [Inject] protected ICheckListService MyService { get; set; }
        [Inject]
        public NavigationManager NavManager { get; set; }

        protected CheckList checkList = new CheckList();

        protected bool formInvalid = true;
        protected EditContext editContext;

        protected async Task RefreshModels()
        {
            lists = await MyService.GetAll();
            IsModalOpened = false;
            Changelists();
            ChangeIsModalOpened();
        }

        protected override async Task OnInitializedAsync()
        {
            editContext = new EditContext(checkList);
            editContext.OnFieldChanged += HandleFieldChanged;
            await RefreshModels();
            checkList.Orden = lists.Count() + 1;
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
            checkList.Orden = lists.Count() + 1;
            editContext = new EditContext(checkList);
            editContext.OnFieldChanged += HandleFieldChanged;
            formInvalid = true;
            ChangeIsModalOpened();
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

        protected void Changelists()
        {
            OnlistsChange.Invoke(lists);
        }

        protected void ChangeIsModalOpened()
        {
            OnIsModalOpenedChange?.Invoke(IsModalOpened);
        }
    }
}