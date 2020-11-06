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
    public class TestBase: ComponentBase
    {
        protected IEnumerable<CheckList> lists { get; set; }
        protected IEnumerable<InstanceChecklist> instancelists { get; set; }
        protected InstanceChecklist instanceCL= new InstanceChecklist(); 
        [Inject] protected ICheckListService MyCheckListService { get; set; }
        [Inject] protected IInstanceChecklistService MyInstanceChecklistService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await RefreshModels();
        }

        protected async Task RefreshModels()
        {
            lists = await MyCheckListService.GetAll();
            instancelists = await MyInstanceChecklistService.GetAll();
        }
        public void InsertInstance(int id_LC, string incicod)
        {

        }

        protected async Task AddInstanceCheckList(InstanceChecklist tempList)
        {
            await MyInstanceChecklistService.InsertInstanceChecklist(tempList);
            await RefreshModels();
        }

        /**
         * Registers the new checklist and navigates to its page
         * */
        protected async Task HandleValidSubmit()
        {
            await AddInstanceCheckList(instanceCL);
           // afterUrl = "/checklist/" + instanceCL.InstanciadoId;
            //NavManager.NavigateTo(afterUrl); // to do: agregar a pagina anterior
        }
    }
}
