using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Multimedia
{
    public partial class MicrophoneComponent
    {
        ElementReference actionButtonRef;
        ElementReference downloadLinkRef;
        
        bool recording = false;

        protected override async Task OnInitializedAsync()
        {
            await JS.InvokeAsync<bool>("initAudio");
            //await JS.InvokeAsync<bool>("setDownloadLink", downloadLinkRef);
        }

        string ActionButtonText()
        {
            return !recording ? "Grabar" : "Detener";
        }

        async Task OnActionButtonClicked()
        {
            recording = !recording;
            await JS.InvokeAsync<bool>("toggleRecording", actionButtonRef, downloadLinkRef);
        }

        async Task OnDownloadButtonClicked()
        {
            await JS.InvokeAsync<bool>("saveAudio");
        }

    }
}
