using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace PRIME_UCR.Components.Controls
{
    public class DatePicker : GenericInput<DateTime?>
    {
        protected override async Task OnChangeEvent(ChangeEventArgs e)
        {
            var valid = DateTime.TryParse((string) e.Value, out var result);
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
            _type = "date";
        }
    }
}