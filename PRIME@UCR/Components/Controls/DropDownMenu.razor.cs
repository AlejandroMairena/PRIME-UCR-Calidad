using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace PRIME_UCR.Components.Controls
{
    public partial class DropDownMenu<T>
    {
        [Parameter]
        public List<T> Data { get; set; }
        
        [Parameter] public string TextProperty { get; set; }

        [Parameter] public string Label { get; set; }
        
        [Parameter] public string DefaultText { get; set; }

        [Parameter] public bool UseValidation { get; set; } = true;

        private int _index = -1;
        private string SelectedValue => _index.ToString();

        public string ValidationCssClass
        {
            get
            {
                if (UseValidation)
                {
                    return ValidationUtils.ToBootstrapValidationCss(CssClass);
                }

                return "";
            }    
        }


        protected override bool TryParseValueFromString(string value, out T result, out string validationErrorMessage)
        {
            var index = Int32.Parse(value);
            result = index >= 0 ? Data[index] : default;
            validationErrorMessage = null;
            return true;
        }

        async Task OnChangeEvent(ChangeEventArgs args)
        {
            _index = Int32.Parse((string)args.Value);
            Value = _index >= 0 ? Data[_index] : default;
            await ValueChanged.InvokeAsync(Value);
            if (UseValidation)
                EditContext.NotifyFieldChanged(FieldIdentifier);
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
            else
            {
                _index = -1;
                if (String.IsNullOrEmpty(DefaultText))
                {
                    throw new InvalidOperationException("The DefaultText parameter must be set when the initial value is set to null.");
                }
            }
        }
    }
}
