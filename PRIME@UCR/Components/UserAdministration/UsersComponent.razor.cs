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
    /**
     * Partial class used to manage the logic part of the UsersComponent.
     */
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

        /**
         * Function: Assigns, a new list of users to the attribute ListUsers once IsInitialized  is set to true.
         */
        protected override void OnInitialized()
        {
            ListUsers = new List<Usuario>();
            ListUsersPerProfile = new List<Persona>();
        }


        /**
         * Function: Assigns, a new list of permissions to the attribute ListUsers based on the users that are
         * placed on the database.
         */
        protected override async Task OnInitializedAsync()
        {
            ListUsers = (await userService.GetAllUsersWithDetailsAsync()).ToList();
        }

        /**
         * Function: Used to update the users asigned to each profile so that the users chosen by the user on the
         * front end, are changed at the database level. 
         * 
         * Requires: The user id, which corresponds to a attribute that uniquely identifies a user, and an argument
         * that indicates that there's an event happening.
         */
        protected async Task update_profile(string IdUser, ChangeEventArgs e)
        {
            if (Value.PermissionsList != null)
            {
                var User = (ListUsers.Find(p => p.Id == IdUser));
                if ((bool)e.Value)
                {
                    Value.UserLists.Add(User);
                    await perteneceService.InsertUserOfProfileAsync(User.Id, Value.ProfileName);
                    Value.StatusMessage = "El usuario " + User.UserName + " fue agregado del perfil " + Value.ProfileName + ". Para notar los cambios, deberá reiniciar su sesión.";
                }
                else
                {
                    Value.UserLists.Remove(User);
                    await perteneceService.DeleteUserOfProfileAsync(User.Id, Value.ProfileName);
                    Value.StatusMessage = "El usuario " + User.UserName + " fue removido del perfil " + Value.ProfileName + ". Para notar los cambios, deberá reiniciar su sesión.";
                }
                Value.CheckedUsers[(Value.CheckedUsers.FindIndex(p => p.Item1 == IdUser))] =  new Tuple<string, bool>(IdUser,(bool)e.Value);
                await ValueChanged.InvokeAsync(Value);
            }
        }

    }
}
