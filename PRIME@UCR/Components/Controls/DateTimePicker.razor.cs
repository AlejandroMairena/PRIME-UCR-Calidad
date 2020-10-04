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

        Task OnDateChanged(DateTime d)
        {
            Value = d + _time;

            return ValueChanged.InvokeAsync(Value);
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