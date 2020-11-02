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

namespace PRIME_UCR.Components.CheckLists
{
    /**
    * This page displays every checklist and general data for each one
    * */
    public class CheckListInstanceAviableBase : ComponentBase
    {
        protected IEnumerable<CheckList> lists { get; set; }

        [Inject] protected ICheckListService MyService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await RefreshModels();
        }

        /**
         * Gets the list of checklists in the database
         * */
        protected async Task RefreshModels()
        {
            lists = await MyService.GetAll();
        }
    }
}
