using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using PRIME_UCR.Application.Services.Multimedia;

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
        public bool ShowVideoComponent { get; set; } = false;
        [Parameter]
        public bool ShowTextComponent { get; set; } = false;
        [Parameter]
        public bool ShowImage { get; set; } = false;
        [Parameter]
        public bool ShowMicrophone { get; set; }
        [Parameter]
        public MultimediaContent MContent { get; set; }
        [Parameter]
        public bool ShowText { get; set; } = false;
        [Parameter]
        public bool ShowAudio { get; set; } = false;
        [Parameter]
        public EventCallback<MultimediaContent> OnFileUpload { get; set; }
        public delegate Task ModalClosed();
        public event ModalClosed OnModalClosed;

        /* Appointment code for auto naming real time multimedia content.
         */
        [Parameter]
        public string ApCode { get; set; }

        public async Task CloseImageView()
        {
            Show = false;

            if (OnModalClosed != null) await OnModalClosed();
            if (MContent != null)
            {
                string pathEncrypted = MContent.Archivo;
                byte[] pathEncryptedByte = System.Convert.FromBase64String(pathEncrypted);
                string pathDecrypted = encrypt_service.Decrypt(pathEncryptedByte);
                encrypt_service.EncryptFile(pathDecrypted);
                MContent = null;
            }
            await OnClose.InvokeAsync(Show);
        }
        string getSrc() {
            //string src = MContent.Archivo; 
            //AQUI HAY QUE DESENCRIPTAR EL ARCHIVO Y EL PATH PARA HACERLO DINAMICO
            string pathEncrypted = MContent.Archivo;
            byte[] pathEncryptedByte = System.Convert.FromBase64String(pathEncrypted);
            string pathDecrypted = encrypt_service.Decrypt(pathEncryptedByte);
            string filename = MContent.Nombre;
            encrypt_service.DecryptFile(pathDecrypted);
            string path = pathDecrypted.Replace("wwwroot/","");
            return path;
        }
        string getName() {
            //MContent tiene el name, hay que sacarlo de ahi para que sea dinamico
            //string src = "practica.png";
            string src = MContent.Nombre;
            return src;
        }
        async Task getText() {//REVISAR
            //AQUI HAY QUE DESENCRIPTAR EL ARCHIVO Y EL PATH PARA HACERLO DINAMICO
            string pathEncrypted = MContent.Archivo;
            byte[] pathEncryptedByte = System.Convert.FromBase64String(pathEncrypted);
            string pathDecrypted = encrypt_service.Decrypt(pathEncryptedByte);
            string filename = MContent.Nombre;
            encrypt_service.DecryptFile(pathDecrypted);

            string path = pathDecrypted.Replace("wwwroot/", "");
           
            //string path = "img/prueba.txt";
            await JS.InvokeAsync<bool>("showTxt", path);



        }
        string getAudio() {
            string pathEncrypted = MContent.Archivo;
            byte[] pathEncryptedByte = System.Convert.FromBase64String(pathEncrypted);
            string pathDecrypted = encrypt_service.Decrypt(pathEncryptedByte);
            string filename = MContent.Nombre;
            encrypt_service.DecryptFile(pathDecrypted);
            string path = pathDecrypted.Replace("wwwroot/", "");
            return path;
        }
        string getVideo() {
            string pathEncrypted = MContent.Archivo;
            byte[] pathEncryptedByte = System.Convert.FromBase64String(pathEncrypted);
            string pathDecrypted = encrypt_service.Decrypt(pathEncryptedByte);
            string filename = MContent.Nombre;
            encrypt_service.DecryptFile(pathDecrypted);
            string path = pathDecrypted.Replace("wwwroot/", "");
            return path;
        }
        


    }
}
