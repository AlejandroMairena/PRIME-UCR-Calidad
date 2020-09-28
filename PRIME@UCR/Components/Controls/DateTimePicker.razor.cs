using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;

namespace PRIME_UCR.Components.Controls
{
    public partial class DateTimePicker
    {
        private TimeSpan _time; 
        
        [Parameter]
        public DateTime Date { get; set; }
        
        [Parameter]
        public EventCallback<DateTime> DateChanged { get; set; }
        
        [Parameter] public string DateLabel { get; set; }
        [Parameter] public string TimeLabel { get; set; }

        Task OnDateChanged(ChangeEventArgs e)
        {
            Date = DateTime.Parse((string)e.Value).Date + _time;

            return DateChanged.InvokeAsync(Date);
        }
        

        Task OnTimeChanged(ChangeEventArgs e)
        {
            _time = TimeSpan.Parse((string)e.Value);
            Date = Date.Date + _time;

            return DateChanged.InvokeAsync(Date);
        }

        protected override void OnInitialized()
        {
            _time = DateTime.Now - DateTime.Today;
        }
    }
}