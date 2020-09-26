using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace PRIME_UCR.Components.Controls
{
    public partial class DropDownMenu<TEnum> where TEnum : notnull
    {
        [Parameter] public List<(TEnum, string)> Values { get; set; }
        [Parameter] public EventCallback<TEnum> OnChangeCallback { get; set; }

        int GetIndex((TEnum, string) tuple)
        {
            return Values.IndexOf(tuple) + 1; // Subrtract one to have values be one based
                                              // otherwise onchange event is not fired for index 0
        }

        async Task OnChange(ChangeEventArgs args)
        {
            var index = Int32.Parse((string)args.Value) - 1; // Subtract one to make it zero based
            if (index >= 0 && index < Values.Count) // only if value is within bounds
                await OnChangeCallback.InvokeAsync(Values[index].Item1);
        }
    }
}
