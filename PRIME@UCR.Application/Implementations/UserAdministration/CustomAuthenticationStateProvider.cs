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

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);

            Usuario user0 = new Usuario();
            user0.Email = user0.UserName = "admin@admin.com";
            String password = "Admin_1234";
            await UserManager.CreateAsync(user0, password);

            return await Task.FromResult(new AuthenticationState(user));
        }

        public async Task<bool> AuthenticateLogin(Usuario usuario)
        {
            var userToCheck = await UserManager.FindByEmailAsync(usuario.Email);

            ClaimsIdentity identity = new ClaimsIdentity();
            
            if(userToCheck != null)
            {
                var loginResult = await SignInManager.CheckPasswordSignInAsync(userToCheck, usuario.Contraseña, false);

                if (loginResult.Succeeded)
                {
                    identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, usuario.Email),
                    }, "apiauth_type");
                } else
                {
                    return await Task.FromResult(false);
                }
            } else
            {
                return await Task.FromResult(false);
            }

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));

            return await Task.FromResult(true);
        }
    }
}