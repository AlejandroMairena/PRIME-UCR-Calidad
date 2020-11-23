using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Pages.Appointments
{
    public partial class AppointmentView
    {
        [Parameter] public string id { get; set; }

        private async Task updatelist(bool f) {
            StateHasChanged(); 
        }
    }
}
