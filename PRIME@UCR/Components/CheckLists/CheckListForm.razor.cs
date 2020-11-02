using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PRIME_UCR.Domain.Models.CheckLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.CheckLists
{
    public partial class CheckListForm
    {
        [Parameter] public CheckList CheckList { get; set; }
        [Parameter] public EventCallback<CheckList> ValidSubmit { get; set; }
        [Parameter] public EventCallback<int> OnDispose { get; set; }
        [Parameter] public bool Update { get; set; }
        protected bool formInvalid = false;
        protected EditContext editContext;
        protected List<string> _types = new List<string>();

        protected override void OnInitialized()
        {
            _types.Add("Colocación equipo");
            _types.Add("Retiro equipo");
            _types.Add("Paciente en origen");
            _types.Add("Paciente en destino");
            _types.Add("Paciente en traslado");
            editContext = new EditContext(CheckList);
            editContext.OnFieldChanged += HandleFieldChanged;
            StateHasChanged();
        }

        protected void HandleFieldChanged(object sender, FieldChangedEventArgs e)
        {
            formInvalid = editContext.Validate();
            if (formInvalid == true)
            {
                StateHasChanged();
            }
        }
        public async Task Dispose()
        {
            editContext.OnFieldChanged -= HandleFieldChanged;
            await OnDispose.InvokeAsync(0);
        }

        protected async Task HandleValidSubmit()
        {
            await ValidSubmit.InvokeAsync(CheckList);
        }
    }
}
