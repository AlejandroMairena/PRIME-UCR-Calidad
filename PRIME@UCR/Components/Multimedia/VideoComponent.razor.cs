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

        // Reference to Multimedia Modal Parent
        [Parameter]
        public MultimediaModal MultimediaModal { get; set; }

        // Element References
        ElementReference startButton;
        ElementReference videoPreview;
        ElementReference recordedVideo;
        ElementReference downloadButton;
        ElementReference stopButton;


        protected override void OnInitialized()
        {
            // add CloseComponent method to OnModalClosed event
            MultimediaModal.OnModalClosed += CloseComponent;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // call JS code to initialize Element References
            await JS.InvokeAsync<bool>("videoInit", startButton, videoPreview, recordedVideo, downloadButton, stopButton);
        }

        async Task CloseComponent()
        {
            //await CloseCamera();
            MultimediaModal.OnModalClosed -= CloseComponent;
        }

    }
}
