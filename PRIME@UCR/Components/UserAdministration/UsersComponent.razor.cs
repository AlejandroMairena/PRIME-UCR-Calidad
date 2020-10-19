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
            ListUsers = (await userService.GetUsuarios()).ToList();
        }

        protected override async Task OnParametersSetAsync()
        {
            var ListProfilesAndUsers = await profileService.GetPerfilesWithDetailsAsync();
            var profile = (ListProfilesAndUsers.Find(p => p.NombrePerfil == Value.ProfileName));
            ListUsersPerProfile.Clear();
            foreach (var users in profile.UsuariosYPerfiles)
            {
                var person = await userService.getPersonWithDetailstAsync(users.Usuario.Email);
                ListUsersPerProfile.Add(person);
            }
            Value.UserLists = ListUsersPerProfile;
        }

        private bool check(string cedPerson)
        {
            if (ListUsersPerProfile.Count != 0)
            {
                return (ListUsersPerProfile.Find(p => p.Cédula == cedPerson) == null) ? false : true;
            }
            return false;
        }

    }
}
