using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Components.CheckLists
{
    public class CheckListNavMenuBase: ComponentBase
    {
        [CascadingParameter(Name = "lists")]
        public IEnumerable<CheckList> lists { get; set; }

        [Parameter]
        public Action<IEnumerable<CheckList>> OnlistsChange { get; set; }

        public bool IsModalOpened { get; set; }

        protected bool collapseNavMenu = true;

        protected string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        protected void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        protected void ToggleIsModalOpened()
        {
            IsModalOpened = !IsModalOpened;
        }

        private void Changelists()
        {
            OnlistsChange?.Invoke(lists);
        }

        protected void Changelists(IEnumerable<CheckList> newlists)
        {
            lists = newlists;
            StateHasChanged();
            Changelists();
        }

        protected void ChangeIsModalOpened(bool newIsModalOpened)
        {
            IsModalOpened = newIsModalOpened;
            StateHasChanged();
        }
    }
}
