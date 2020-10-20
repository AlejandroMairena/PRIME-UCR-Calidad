using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.UserAdministration
{
    public partial class UsersComponent
    {
        [Inject]
        public IUserService userService { get; set; }

        public List<Usuario> ListUsers { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ListUsers = (await userService.GetUsuarios()).ToList();
        }
    }
}
