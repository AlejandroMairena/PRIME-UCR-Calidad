using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Multimedia
{
    public partial class VideoComponent
    {

        [Parameter]
        public MultimediaModal MultimediaModal { get; set; }

        ElementReference startButton;
        ElementReference videoPreview;
        ElementReference recordedVideo;
        ElementReference downloadButton;
        ElementReference stopButton;


        protected override void OnInitialized()
        {
            MultimediaModal.OnModalClosed += CloseComponent;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JS.InvokeAsync<bool>("videoInit", startButton, videoPreview, recordedVideo, downloadButton, stopButton);
        }

        async Task CloseComponent()
        {
            //await CloseCamera();
            MultimediaModal.OnModalClosed -= CloseComponent;
        }

    }
}
