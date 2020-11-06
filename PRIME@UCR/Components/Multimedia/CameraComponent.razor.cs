using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PRIME_UCR.Components.Multimedia
{
    public partial class CameraComponent
    {
        ElementReference videoElement;
        ElementReference canvasElement;

        protected override async Task OnInitializedAsync()
        {
            //await OpenCamera();
        }

        async Task<bool> OpenCamera()
        {
            return await JS.InvokeAsync<bool>("openCamera", videoElement);
        }

        async void TakePhotograph()
        {
            await JS.InvokeAsync<bool>("takePhotograph", canvasElement);
        }

    }
}
