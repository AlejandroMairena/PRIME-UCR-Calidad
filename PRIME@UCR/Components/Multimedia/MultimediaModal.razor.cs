using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Multimedia
{
    public partial class MultimediaModal
    {
        [Parameter]
        public bool Show { get; set; }
        [Parameter]
        public EventCallback<bool> OnClose { get; set; }


        [Parameter]
        public bool ShowCamera { get; set; } = false;
        [Parameter]
        public bool ShowVideo { get; set; } = false;
        [Parameter]
        public bool ShowImage { get; set; } = false;
        [Parameter]
        public MultimediaContent MContent { get; set; }


        async Task CloseImageView()
        {
            Show = false;
            await OnClose.InvokeAsync(Show);
        }

    }
}
