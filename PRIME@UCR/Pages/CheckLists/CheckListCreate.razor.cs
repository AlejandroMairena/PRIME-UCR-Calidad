﻿using Microsoft.AspNetCore.Components;
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
    public class CheckListCreateBase : ComponentBase
    {
   
        protected IEnumerable<CheckList> lists { get; set; }

        [Inject] protected ICheckListService MyService { get; set; }

        [Inject]  public NavigationManager NavManager { get; set; }

        private const string CreateUrl = "/checklist/create";
        private string beforeUrl = "/checklist"; //mejorar diseño de interfaz
        private string afterUrl = "";

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

        protected async Task AddCheckList(CheckList tempList)
        {
            await MyService.InsertCheckList(tempList);
            await RefreshModels();
        }


        protected async Task HandleValidSubmit()
        {
            await AddCheckList(checkList);
            afterUrl = "/checklist/" + checkList.Id;
            NavManager.NavigateTo(afterUrl); // to do: agregar a pagina anterior
        }

    }
}
