using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace PRIME_UCR.Components.Controls
{
    public partial class TextBox
    {
        [Parameter]
        public string Label { get; set; }

        private string _value;

        public string ValidationCssClass => ValidationUtils.ToBootstrapValidationCss(CssClass);

        protected override bool TryParseValueFromString(string value, out string result, out string validationErrorMessage)
        {
            result = value;
            validationErrorMessage = "";
            return true;
        }
        
        async Task OnChangeEvent(ChangeEventArgs args)
        {
            Value = (string) args.Value;
            await ValueChanged.InvokeAsync(Value);
            EditContext.NotifyFieldChanged(FieldIdentifier);
        }
    }
}
