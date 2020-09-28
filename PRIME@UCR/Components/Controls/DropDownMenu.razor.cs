using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace PRIME_UCR.Components.Controls
{
    public partial class DropDownMenu<T>
    {
        [Parameter]
        public List<T> Data { get; set; }

        [Parameter]
        public T Value { get; set; }  // used for data binding
        
        [Parameter]
        public string TextProperty { get; set; }
        
        [Parameter]
        public EventCallback<T> ValueChanged { get; set; }

        [Parameter]
        public string Label { get; set; }

        private int _index = 0;

        async Task OnChangeEvent(ChangeEventArgs args)
        {
            _index = Int32.Parse((string) args.Value);
            Value = Data[_index];
            await ValueChanged.InvokeAsync(Value);
        }

        string GetText(T value)
        {
            if (String.IsNullOrEmpty(TextProperty))
            {
                return value.ToString();
            }
            
            return (string) typeof(T).GetProperty(TextProperty)?.GetValue(value);
        }
        protected override void OnParametersSet()
        {
            if (Value != null)
            {
                _index = Data.IndexOf(Value);
            }
        }
    }
}
