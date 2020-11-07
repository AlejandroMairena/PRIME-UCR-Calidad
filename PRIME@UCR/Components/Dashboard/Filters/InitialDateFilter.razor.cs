using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRIME_UCR.Application.DTOs.Dashboard;

namespace PRIME_UCR.Components.Dashboard.Filters
{
    public partial class InitialDateFilter
    {
        [Parameter] public FilterModel Value { get; set; }
        [Parameter] public EventCallback<FilterModel> ValueChanged { get; set; }

        private async Task OnPickedDate(DateTime? date)
        {
            Value.InitialDateFilter = date;
            await ValueChanged.InvokeAsync(Value);
        }

    }
}
