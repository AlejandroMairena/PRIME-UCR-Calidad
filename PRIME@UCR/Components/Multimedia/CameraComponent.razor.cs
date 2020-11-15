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
        [Parameter]
        public MultimediaModal MultimediaModal { get; set; }

        ElementReference videoElement;
        ElementReference canvasElement;
        ElementReference imageElement;
        ElementReference downloadLinkRef;
        bool cameraOpen = false;
        bool photoTaken = false;

        protected override async Task OnInitializedAsync()
        {
            //await OpenCamera();
        }

        protected override void OnInitialized()
        {
            MultimediaModal.OnModalClosed += CloseComponent;
        }

        async Task HandleClick()
        {
            if (!cameraOpen)
            {
                await OpenCamera();
            }
            else if (cameraOpen && !photoTaken)
            {
                await TakePhotograph();
            }
            else
            {
                CancelPhotograph(); 
            }
        }

        async Task OpenCamera()
        {
            await JS.InvokeAsync<bool>("openCamera", videoElement);
            cameraOpen = true;
        }

        async Task CloseComponent()
        {
            await CloseCamera();
            MultimediaModal.OnModalClosed -= CloseComponent;
        }

        async Task<bool> CloseCamera()
        {
            return await JS.InvokeAsync<bool>("closeCamera", videoElement);
        }
        async Task TakePhotograph()
        {
            photoTaken = true;
            await JS.InvokeAsync<string>("takePhotograph", canvasElement, videoElement, imageElement, downloadLinkRef);
        }

        void CancelPhotograph()
        {
            photoTaken = false;
        }

        string VideoClass()
        {
            return photoTaken ? "hidden" : "rt-box";
        }
        string CanvasClass()
        {
            return !photoTaken ? "hidden" : "rt-box";
        }

        string ButtonText()
        {
            if (!cameraOpen)
            {
                return "Abrir Cámara";
            }
            else if (cameraOpen && !photoTaken)
            {
                return "Tomar Fotografía";
            }
            else
            {
                return "Cancelar";
            }
        }
        string DowloadLinkClass()
        {
            return photoTaken ? "btn btn-primary" : "btn btn-primary hidden";
        }

    }
}
