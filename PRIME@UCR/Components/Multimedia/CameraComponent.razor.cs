﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PRIME_UCR.Components.Multimedia
{

    public partial class CameraComponent
    {
        [Parameter]
        public MultimediaModal MultimediaModal { get; set; }

        ElementReference videoElement;
        ElementReference canvasElement;
        ElementReference imageElement;
        ElementReference downloadLinkRef;
        bool cameraOpen = false;
        bool cameraClose => !cameraOpen;
        bool photoTaken = false;
        bool photoNotTaken => !photoTaken;

        protected override void OnInitialized()
        {
            MultimediaModal.OnModalClosed += CloseComponent;
        }

        // Open Close Camera Button Code
        async Task HandleOpenCloseButtonClick()
        {
            if (!cameraOpen) await OpenCamera();
            else await CloseCamera();
        }
        async Task OpenCamera()
        {
            await JS.InvokeAsync<bool>("openCamera", videoElement);
            cameraOpen = true;
        }
        async Task CloseCamera()
        {
            await JS.InvokeAsync<bool>("closeCamera", videoElement);
            cameraOpen = false;
        }
        string OpenCloseButtonText()
        {
            return !cameraOpen ? "Abrir Cámara" : "Cerrar Cámara";
        }


        async Task TakePhotograph()
        {
            await JS.InvokeAsync<string>("takePhotograph", canvasElement, videoElement, imageElement, downloadLinkRef);
            photoTaken = true;
        }
        async Task CancelPhotograph()
        {
            photoTaken = false;
            await JS.InvokeAsync<bool>("clearCanvas", canvasElement);
        }
        async Task CloseComponent()
        {
            await CloseCamera();
            MultimediaModal.OnModalClosed -= CloseComponent;
        }
    }
}