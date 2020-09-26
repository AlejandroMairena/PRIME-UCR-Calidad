using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace PRIME_UCR.Components.Controls
{
    public partial class TabItem<TEnum> where TEnum : notnull
    {
        [Parameter]
        public TEnum Tab { get; set; }
        [Parameter]
        public TEnum CurrentTab { get; set; }
        [Parameter]
        public string TabName { get; set; }
        [Parameter]
        public EventCallback<TEnum> OnTabSetCallback { get; set; }

        async Task OnTabSet()
        {
            await OnTabSetCallback.InvokeAsync(Tab);
        }

        private string activeClass;

        void SetActiveClass()
        {
            activeClass = Tab.Equals(CurrentTab) ? "active" : "";
        }
    }
}