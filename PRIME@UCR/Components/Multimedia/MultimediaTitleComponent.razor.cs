using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Multimedia
{
    public partial class MultimediaTitleComponent
    {
        public string Title { get; set; } = "";
        public bool Error { get; set; }
        public string ErrorMessage = "";

        [Parameter]
        public EventCallback<bool> OnTitleChanged { get; set; }

        public async Task OnChange(ChangeEventArgs e)
        {
            Title = e.Value.ToString();
            Error = false;
            if (Title != null)
            {
                ErrorMessage = "";
                bool error1 = Regex.IsMatch(Title, "[ [ {})(*&^%$#@=!;,.<>]");
                if (error1) ErrorMessage = "Caracteres no válidos.";
                bool error2 = Title.Length > 200;
                if (error2) ErrorMessage = "El título no puede tener más de 200 caracteres.";
                Error = error1 || error2;
            }
            else
            {
                Error = true;
            }
            await OnTitleChanged.InvokeAsync(Error);
            
        }


    }
}
