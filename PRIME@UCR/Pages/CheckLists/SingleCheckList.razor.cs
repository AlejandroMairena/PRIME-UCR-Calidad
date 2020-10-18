using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services;
using PRIME_UCR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Application.Services.CheckLists;
using PRIME_UCR.Components.CheckLists;
using Microsoft.AspNetCore.Components.Forms;
using PRIME_UCR.Domain.Models.CheckLists;

namespace PRIME_UCR.Pages.CheckLists
{
    public class SingleCheckListBase : ComponentBase
    {
        [Parameter]
        public int id { get; set; }

        private bool isDisabled { get; set; } = true;

        protected IEnumerable<CheckList> lists { get; set; }

        public CheckList list { get; set; }

        [Inject] protected ICheckListService MyCheckListService { get; set; }

        [Inject] protected IItemService MyItemService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await RefreshModels();
        }

        protected async Task RefreshModels()
        {
            list = await MyCheckListService.GetById(id);
            lists = await MyCheckListService.GetAll();
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