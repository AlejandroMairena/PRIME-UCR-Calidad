using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace PRIME_UCR.Components.Controls
{
    public partial class GenericInput<TValue>
    {
        [Parameter] public string Label { get; set; }
        
        //If true a red * will appear next to the lable
        [Parameter] public bool IsRequired { get; set; }

        protected string _type = "text";

        public string ValidationCssClass => ValidationUtils.ToBootstrapValidationCss(CssClass);

        protected override bool TryParseValueFromString(string value, out TValue result, out string validationErrorMessage)
        {
            result = Value;
            validationErrorMessage = "";
            return true;
        }

        protected virtual Task OnChangeEvent(ChangeEventArgs e)
        {
            throw new NotImplementedException("Must override OnChangeEvent");
        }
    }
}
