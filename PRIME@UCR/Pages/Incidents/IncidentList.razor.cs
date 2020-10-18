using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Components.Incidents.IncidentDetails.Tabs;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Pages.Incidents
{
    public partial class IncidentList
    {
        private bool exists = false;

        private SampleData[] data;
        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(250);
            data = Array.Empty<SampleData>();
        }

        public class SampleData
        {
            public int? id { get; set; }
            public string full_name { get; set; }
        }
    }

}


