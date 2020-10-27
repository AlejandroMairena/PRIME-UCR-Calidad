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
        protected List<string> _types = new List<string>();

        protected bool formInvalid = true;
        protected EditContext editContext;

        /**
         * Gets the list of checklists in the database
         * */
        protected async Task RefreshModels()
        {
            lists = await MyService.GetAll();
        }

        protected override async Task OnInitializedAsync()
        {
            _types.Add("Colocación equipo");
            _types.Add("Retiro equipo");
            _types.Add("Paciente en origen");
            _types.Add("Paciente en destino");
            _types.Add("Paciente en traslado");
            editContext = new EditContext(checkList);
            editContext.OnFieldChanged += HandleFieldChanged;
            await RefreshModels();
            checkList.Orden = lists.Count() + 1;
        }

        /**
         * Checks if the required fields of the checklist are valid
         * */
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
        }

        /**
         * Registers the new checklist and navigates to its page
         * */
        protected async Task HandleValidSubmit()
        {
            await AddCheckList(checkList);
            afterUrl = "/checklist/" + checkList.Id;
            NavManager.NavigateTo(afterUrl); // to do: agregar a pagina anterior
        }

    }
}
