using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace PRIME_UCR.Components.Controls
{
    public class TextInput : GenericInput<String>
    {
        [Parameter] public string Type { get; set; }
        protected override async Task OnChangeEvent(ChangeEventArgs e)
        {
            string result = (String)e.Value;
            var valid = !String.IsNullOrEmpty(result);
            if (valid)
            {
                Value = result;
            }
            else
            {
                Value = null;
            }

            await ValueChanged.InvokeAsync(Value);
            EditContext.NotifyFieldChanged(FieldIdentifier);
        }

        protected override void OnInitialized()
        {
            _type = Type;
        }
    }
}