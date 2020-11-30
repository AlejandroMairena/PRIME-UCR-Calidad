using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Dashboard.Filters
{
    public partial class GenericIndividualFilterRemover
    {
        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public string ExtraLabel  { get; set; }

        [Parameter]
        public char Value { get; set; }

        [Parameter]
        public EventCallback<char> ValueChanged { get; set; }

        private async Task FilterRemoved()
        {
            await ValueChanged.InvokeAsync(Value);
        }
    }
}
