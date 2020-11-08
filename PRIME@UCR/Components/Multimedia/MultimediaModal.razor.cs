using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
        [Parameter]
        public bool ShowText { get; set; } = false;
        [Parameter]
        public bool ShowAudio { get; set; } = false;
        async Task CloseImageView()
        {
            Show = false;
            await OnClose.InvokeAsync(Show);
        }
        string getSrc() {
            //string src = MContent.Archivo; 
            //AQUI HAY QUE DESENCRIPTAR EL ARCHIVO Y EL PATH PARA HACERLO DINAMICO
            string src = "img/practica.png";
            return src;
        }
        string getName() {
            //MContent tiene el name, hay que sacarlo de ahi para que sea dinamico
            string src = "practica.png";
            return src;
        }
        async Task getText() {
            //AQUI HAY QUE DESENCRIPTAR EL ARCHIVO Y EL PATH PARA HACERLO DINAMICO
            //string path = MContent.Archivo;
            string path = "img/prueba.txt";
            await JS.InvokeAsync<bool>("showTxt", path);
        }
        string getAudio() {
            string path = "img/prueba.mp3";
            return path;
        }
        string getVideo() {
            string path = "img/prueba.mp4";
            return path;
        }

    }
}
