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
        public bool ShowImage { get; set; } = false;
        [Parameter]
        public MultimediaContent MContent { get; set; }
        [Parameter]
        public bool ShowText { get; set; } = false;
        [Parameter]
        public bool ShowAudio { get; set; } = false;
        [Parameter]
        public IEncryptionService ES { get; set; }

        async Task CloseImageView()
        {
            Show = false;
            await OnClose.InvokeAsync(Show);


        }
        string getSrc() {
            //string src = MContent.Archivo; 
            //AQUI HAY QUE DESENCRIPTAR EL ARCHIVO Y EL PATH PARA HACERLO DINAMICO
            string pathEncrypted = MContent.Archivo;
            byte[] pathEncryptedByte = System.Convert.FromBase64String(pathEncrypted);
            setKeyIV();
            string pathDecrypted = encrypt_service.Decrypt(pathEncryptedByte);

            //string path = "img/prueba.mp4";
            return pathDecrypted;
        }
        string getName() {
            //MContent tiene el name, hay que sacarlo de ahi para que sea dinamico
            //string src = "practica.png";
            string src = MContent.Nombre;
            return src;
        }
        async Task getText() {
            //AQUI HAY QUE DESENCRIPTAR EL ARCHIVO Y EL PATH PARA HACERLO DINAMICO
            string pathEncrypted = MContent.Archivo;
            byte[] pathEncryptedByte = System.Convert.FromBase64String(pathEncrypted);
            setKeyIV();
            string pathDecrypted = encrypt_service.Decrypt(pathEncryptedByte);

            //string path = "img/prueba.txt";
            await JS.InvokeAsync<bool>("showTxt", pathDecrypted);



        }
        string getAudio() {
            string pathEncrypted = MContent.Archivo;
            byte[] pathEncryptedByte = System.Convert.FromBase64String(pathEncrypted);
            setKeyIV();
            string pathDecrypted = encrypt_service.Decrypt(pathEncryptedByte);
            string filename = MContent.Nombre;
            encrypt_service.DecryptFile(pathDecrypted);

            //string path = "img/prueba.mp4";
            return pathDecrypted;
        }
        string getVideo() {
            string pathEncrypted = MContent.Archivo;
            byte[] pathEncryptedByte = System.Convert.FromBase64String(pathEncrypted);
            setKeyIV();
            string pathDecrypted = encrypt_service.Decrypt(pathEncryptedByte);

            //string path = "img/prueba.mp4";
            return pathDecrypted;
        }
        void setKeyIV() {
            //string keyString = Configuration.GetConnectionString("Key");
            //string ivString = Configuration.GetConnectionString("IV");
            string keyString = "qXOctUgD1RQCyF6dl4IjgZLAosrLh8Dn8GCklADSmvo=";
            string ivString = "fkmYijInbe9eWQbLoWtTNQ==";
            byte[] ivByte = System.Convert.FromBase64String(ivString);
            byte[] keyByte = System.Convert.FromBase64String(keyString);
            encrypt_service.SetKeyIV(ivByte, keyByte);
        }


    }
}
