﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Application.DTOs.UserAdministration;
using Microsoft.AspNetCore.Identity;
using Blazored.SessionStorage;

namespace PRIME_UCR.Application.Implementations.UserAdministration
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService SessionStorageService;

        protected readonly SignInManager<Usuario> SignInManager;

        protected readonly UserManager<Usuario> UserManager;

        public CustomAuthenticationStateProvider(SignInManager<Usuario> _signInManager, UserManager<Usuario> _userManager, ISessionStorageService _sessionStorageService)
        {
            SignInManager = _signInManager;
            UserManager = _userManager;
            SessionStorageService = _sessionStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();

            var emailAddress = await SessionStorageService.GetItemAsync<string>("emailAddress");

            if (emailAddress != null)
            {
                identity = new ClaimsIdentity(new[]
                {
                        new Claim(ClaimTypes.Name, emailAddress),
                    }, "apiauth_type");
            }
            
            var user = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(user));
        }

        public async Task<bool> AuthenticateLogin(LogInModel logInInfo)
        {
            var userToCheck = await UserManager.FindByEmailAsync(logInInfo.Correo);

            ClaimsIdentity identity = new ClaimsIdentity();
            
            if(userToCheck != null)
            {
                var loginResult = await SignInManager.CheckPasswordSignInAsync(userToCheck, logInInfo.Contraseña, false);

                if (loginResult.Succeeded)
                {
                    identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, logInInfo.Correo),
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
        
        public async Task<bool> Logout()
        {
            await SessionStorageService.RemoveItemAsync("emailAddress");

            ClaimsIdentity identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));

            return await Task.FromResult(true);
        }

    }
}