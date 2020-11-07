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
        ElementReference videoElement;
        ElementReference canvasElement;
        ElementReference imageElement;
        bool cameraOpen = false;
        bool photoTaken = false;

        protected override async Task OnInitializedAsync()
        {
            //await OpenCamera();
        }

        void HandleClick()
        {
            if (!cameraOpen)
            {
                OpenCamera();
            }
            else if (cameraOpen && !photoTaken)
            {
                TakePhotograph();
            }
            else
            {
                CancelPhotograph(); 
            }
        }

        async Task<bool> OpenCamera()
        {
            cameraOpen = true;
            return await JS.InvokeAsync<bool>("openCamera", videoElement);
        }

        async Task<bool> CloseCamera()
        {
            return await JS.InvokeAsync<bool>("closeCamera", videoElement);
        }
        string test = "";
        async Task TakePhotograph()
        {
            photoTaken = true;
            test = await JS.InvokeAsync<string>("takePhotograph", canvasElement, videoElement, imageElement);
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
    }
}
