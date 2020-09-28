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

        protected readonly SignInManager<Usuario> SignInManager;

        protected readonly UserManager<Usuario> UserManager;

        public CustomAuthenticationStateProvider(SignInManager<Usuario> _signInManager, UserManager<Usuario> _userManager)
        {
            SignInManager = _signInManager;
            UserManager = _userManager;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(user));
        }

        public async Task<bool> AuthenticateLogin(Usuario usuario)
        {
            usuario.UserName = usuario.Email;
            var result = await UserManager.CreateAsync(usuario, usuario.Contraseña);

            var loginResult = await SignInManager.CheckPasswordSignInAsync(usuario, usuario.Contraseña, false);

            ClaimsIdentity identity = new ClaimsIdentity();

            if (loginResult.Succeeded)
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, usuario.Email),
                }, "apiauth_type");

            }
            var user = new ClaimsPrincipal(identity);
           

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));

            return await Task.FromResult(true);
        }


    }
}