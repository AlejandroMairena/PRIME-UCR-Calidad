using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRIME_UCR.Application.DTOs.Dashboard;

namespace PRIME_UCR.Components.Dashboard.Filters
{
    public partial class DateFilter
    {
        [Parameter] public FilterModel Value { get; set; }
        [Parameter] public EventCallback<FilterModel> ValueChanged { get; set; }

    }
}
