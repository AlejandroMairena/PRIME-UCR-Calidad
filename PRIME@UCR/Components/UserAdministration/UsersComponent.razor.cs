using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRIME_UCR.Application.DTOs.UserAdministration;

namespace PRIME_UCR.Components.UserAdministration
{
    public partial class UsersComponent
    {
        [Inject]
        public IUserService userService { get; set; }

        [Inject]
        public IProfilesService profileService { get; set; }

        [Inject]
        public IPerteneceService perteneceService { get; set; }

        public List<Usuario> ListUsers { get; set; }

        private List<Persona> ListUsersPerProfile { get; set; }

        [Parameter]
        public ProfileModel Value { get; set; }

        [Parameter]
        public EventCallback<ProfileModel> ValueChanged { get; set; }

        protected override void OnInitialized()
        {
            ListUsers = new List<Usuario>();
            ListUsersPerProfile = new List<Persona>();
        }

        protected override async Task OnInitializedAsync()
        {
            ListUsers = (await userService.GetAllUsersWithDetailsAsync()).ToList();
        }

        protected async Task update_profile(string IdUser, ChangeEventArgs e)
        {
            if (Value.PermissionsList != null)
            {
                var User = (ListUsers.Find(p => p.Id == IdUser));
                if ((bool)e.Value)
                {
                    Value.UserLists.Add(User);
                    await perteneceService.InsertUserOfProfileAsync(User.Id, Value.ProfileName);
                    Value.StatusMessage = "El usuario " + User.UserName + " fue agregado del perfil " + Value.ProfileName;
                }
                else
                {
                    Value.UserLists.Remove(User);
                    await perteneceService.DeleteUserOfProfileAsync(User.Id, Value.ProfileName);
                    Value.StatusMessage = "El usuario " + User.UserName + " fue removido del perfil " + Value.ProfileName;
                }
                await ValueChanged.InvokeAsync(Value);
            }
        }

    }
}
