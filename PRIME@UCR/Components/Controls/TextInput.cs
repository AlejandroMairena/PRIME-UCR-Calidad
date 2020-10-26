using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace PRIME_UCR.Components.Controls
{
    //Implementation of GenericInput used in login form
    public class TextInput : GenericInput<String>
    {
        [Parameter] public string Type { get; set; }

        //On Changed validation for the Input
        //Only validates if inputs in not null or empty
        // Other validations are made with the selected model on implementation
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

        //Override to change the type of input to be used
        protected override void OnInitialized()
        {
            _type = Type;
        }
    }
}