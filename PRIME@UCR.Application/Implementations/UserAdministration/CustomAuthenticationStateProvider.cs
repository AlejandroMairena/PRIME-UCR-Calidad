using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PRIME_UCR.Domain.Models.UserAdministration;
using Microsoft.AspNetCore.Identity;
namespace PRIME_UCR.Application.Implementations.UserAdministration
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        [Inject]
        public SignInManager<Usuario> SignInManager { get; set; }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(user));
        }

        public async Task<bool> AuthenticateLogin(Usuario usuario)
        {
            /*
            var loginResult = await SignInManager.PasswordSignInAsync(usuario.Email, usuario.PasswordHash, true, true);

            ClaimsIdentity identity = new ClaimsIdentity();

            if (loginResult.Succeeded)
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, usuario.Email),
                }, "apiauth_type");

            } */
            var identity = new ClaimsIdentity(new[]
               {
                    new Claim(ClaimTypes.Name, "ate@ate.com"),
                }, "apiauth_type");
            var user = new ClaimsPrincipal(identity);
           

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));

            return await Task.FromResult(true);
        }


    }
}
