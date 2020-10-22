using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;

namespace PRIME_UCR.Components.Controls
{
    public partial class DateTimePicker
    {
        private TimeSpan _time; 
        [Parameter] public string DateLabel { get; set; }
        [Parameter] public string TimeLabel { get; set; }
        private string ValidationCssClass => ValidationUtils.ToBootstrapValidationCss(CssClass);

        async Task OnDateChanged(ChangeEventArgs e)
        {
            DateTime d = DateTime.Parse((string)e.Value);
            Value = d + _time;

            await ValueChanged.InvokeAsync(Value);
            EditContext.NotifyFieldChanged(FieldIdentifier);
        }

        async Task OnTimeChanged(ChangeEventArgs e)
        {
            _time = TimeSpan.Parse((string)e.Value);
            Value = Value.Date + _time;

            await ValueChanged.InvokeAsync(Value);
            EditContext.NotifyFieldChanged(FieldIdentifier);
        }

        protected override void OnInitialized()
        {
            _time = Value.TimeOfDay;
        }

        protected override bool TryParseValueFromString(string value, out DateTime result, out string validationErrorMessage)
        {
            result = DateTime.Parse(value);
            validationErrorMessage = null;
            return true;
        }
    }
}