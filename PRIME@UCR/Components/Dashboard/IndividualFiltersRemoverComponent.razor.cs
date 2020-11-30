using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.DTOs.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Dashboard
{
    public partial class IndividualFiltersRemoverComponent
    {
        [Parameter]
        public FilterModel FilterInfo { get; set; }

        [Parameter]
        public EventCallback<FilterModel> FilterInfoChanged { get; set; }

        private void Test()
        {

        }
    }
}
