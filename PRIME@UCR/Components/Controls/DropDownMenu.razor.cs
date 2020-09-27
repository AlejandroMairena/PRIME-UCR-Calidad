using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace PRIME_UCR.Components.Controls
{
    public partial class DropDownMenu<T>
    {
        [Parameter]
        public List<Tuple<T, string>> Values { get; set; }

        [Parameter]
        public EventCallback<T> OnChange { get; set; }

        [Parameter]
        public string Label { get; set; }

        int GetIndex(Tuple<T, string> tuple)
        {
            return Values.IndexOf(tuple) + 1; // Subrtract one to have values be one based
                                              // otherwise onchange event is not fired for index 0
        }

        async Task OnChangeEvent(ChangeEventArgs args)
        {
            var index = Int32.Parse((string)args.Value) - 1; // Subtract one to make it zero based
            if (index >= 0 && index < Values.Count) // only if value is within bounds
                await OnChange.InvokeAsync(Values[index].Item1);
        }
    }
}
