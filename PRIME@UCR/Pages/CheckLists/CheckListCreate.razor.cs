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
    /**
    * This page displays a form to create a new checklist
    * */
    public class CheckListCreateBase : ComponentBase
    {
   
        protected IEnumerable<CheckList> lists { get; set; }

        [Inject] protected ICheckListService MyService { get; set; }

        [Inject]  public NavigationManager NavManager { get; set; }

        private const string CreateUrl = "/checklist/create";
        // private string beforeUrl = "/checklist"; //mejorar diseño de interfaz
        private string afterUrl = "";

        protected CheckList checkList = new CheckList();


        /**
         * Gets the list of checklists in the database
         * */
        protected async Task RefreshModels()
        {
            lists = await MyService.GetAll();
        }

        protected override async Task OnInitializedAsync()
        {
            await RefreshModels();
            checkList.Orden = lists.Count() + 1;
        }

        /**
         * Checks if the required fields of the checklist are valid
         * */



        public void Dispose()
        {
            NavManager.NavigateTo("/checklist"); // to do: agregar a pagina anterior
        }

        /**
         * Inserts the new checklist into the database
         * */
        protected async Task AddCheckList(CheckList tempList)
        {
            if (tempList.ImagenDescriptiva == null)
            {
                tempList.ImagenDescriptiva = "/images/defaultCheckList.svg";
            }
            await MyService.InsertCheckList(tempList);
            await RefreshModels();
            afterUrl = "/checklist/" + checkList.Id;
            NavManager.NavigateTo(afterUrl); // to do: agregar a pagina anterior
        }

        /**
         * Registers the new checklist and navigates to its page
         * */

    }
}
