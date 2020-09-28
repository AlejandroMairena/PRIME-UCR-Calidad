using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using PRIME_UCR.Application.Implementations.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.UserAdministration
{
    public partial class LoginForm
    {
        [Inject]
        public AuthenticationStateProvider MyAuthenticationStateProvider { get; set; }

        public Usuario usuario { get; set; }

        protected override void OnInitialized()
        {
            usuario = new Usuario();
            base.OnInitialized();
        }

        private async Task<bool> ValidateUser()
        {
            //DB
            // var authResult = await SignInManager.PasswordSignInAsync(usuario.Email, usuario.PasswordHash, true, true);
            var result = await ((CustomAuthenticationStateProvider)MyAuthenticationStateProvider).AuthenticateLogin(usuario);

            await sessionStorage.SetItemAsync("emailAddress",usuario.Email);

            return await Task.FromResult(result);
        }
        
           
        
    }
}
