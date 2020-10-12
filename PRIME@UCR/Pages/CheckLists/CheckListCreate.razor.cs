using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services;
using PRIME_UCR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Application.Services.CheckLists;
using PRIME_UCR.Components.CheckLists;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq;


namespace PRIME_UCR.Pages.CheckLists
{
    public class CheckListCreateBase : ComponentBase
    {
        [CascadingParameter(Name = "lists")]
        protected IEnumerable<CheckList> lists { get; set; }

        [Inject] protected ICheckListService MyService { get; set; }

        private const string CreateUrl = "/checklist/create";
        private string beforeUrl = "/checklist"; //mejorar diseño de interfaz
    }
}
