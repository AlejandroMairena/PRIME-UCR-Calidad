using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using PRIME_UCR.Application.Implementations.UserAdministration;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace PRIME_UCR.Components.UserAdministration
{
    public partial class LoginForm
    {
        [Inject]
        public AuthenticationStateProvider MyAuthenticationStateProvider { get; set; }

        public LogInModel logInInfo;

        string invalidUser = "hide";

        EditContext _context;

        bool isBusy = false;

        bool isFormValid = false;

        protected override void OnInitialized()
        {
            logInInfo = new LogInModel();
            base.OnInitialized();
        }

        protected override Task OnInitializedAsync()
        {
            _context = new EditContext(logInInfo);
             _context.OnFieldChanged += HandleFieldChanged;
            return base.OnInitializedAsync();
        }

        // used to toggle submit button disabled attribute
        private void HandleFieldChanged(object sender, FieldChangedEventArgs e)
        {
            isFormValid = _context.Validate();
            StateHasChanged();
        }

        private async Task<bool> ValidateUser()
        {
            isBusy = true;
            await Task.Delay(1000); //Testing loading indicator
            //DB
            // var authResult = await SignInManager.PasswordSignInAsync(usuario.Email, usuario.PasswordHash, true, true);
            var result = await ((CustomAuthenticationStateProvider)MyAuthenticationStateProvider).AuthenticateLogin(logInInfo);

            if(result == false)
            {
                invalidUser = "show";
            }

            await sessionStorage.SetItemAsync("emailAddress",logInInfo.Correo);

            isBusy = false;
            return await Task.FromResult(result);
        }
        
           
        
    }
}
