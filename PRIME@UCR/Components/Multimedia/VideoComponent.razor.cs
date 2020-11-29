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
        ElementReference closeButton;

        protected override void OnInitialized()
        {
            // add CloseComponent method to OnModalClosed event
            if (MultimediaModal != null)
                MultimediaModal.OnModalClosed += CloseComponent;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // call JS code to initialize Element References
            await JS.InvokeAsync<bool>("videoInit", startButton, videoPreview, recordedVideo, downloadButton, stopButton, closeButton);
        }

        async Task CloseComponent()
        {
            if (MultimediaModal != null)
                MultimediaModal.OnModalClosed -= CloseComponent;
        }

        // Open/Close Button
        bool cameraOpen = false;
        bool cameraClose => !cameraOpen;
        string openCloseButtonText => !cameraOpen ? "Abrir Cámara" : "Cerrar Cámara";

        async Task HandleOpenCloseClick()
        {
            if (!cameraOpen)
                await JS.InvokeAsync<bool>("openCamera", videoPreview);
            else
            {
                await JS.InvokeAsync<bool>("stop", videoPreview, true);
                await JS.InvokeAsync<bool>("closeCamera", videoPreview);
            }
            cameraOpen = !cameraOpen;
        }

        async Task OnClose()
        {
            await JS.InvokeAsync<bool>("stop", videoPreview, true);
            MultimediaModal?.CloseImageView();
        }

        async void OnTitleChanged(Tuple<bool, string> tuple)
        {
            if (!tuple.Item1) // if valid file name
                await JS.InvokeAsync<bool>("setDownloadName", tuple.Item2);
        }

    }
}
