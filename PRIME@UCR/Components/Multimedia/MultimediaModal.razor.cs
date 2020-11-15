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
        public bool ShowMicrophone { get; set; }
        [Parameter]
        public MultimediaContent MContent { get; set; }
        [Parameter]
        public bool ShowText { get; set; } = false;
        [Parameter]
        public bool ShowAudio { get; set; } = false;
        [Parameter]
        public IEncryptionService ES { get; set; }

        public delegate Task ModalClosed();
        public event ModalClosed OnModalClosed;

        async Task CloseImageView()
        {
            Show = false;

            if (OnModalClosed != null) await OnModalClosed();
            if (ShowMicrophone == true || ShowCamera == true)
            {
                await OnClose.InvokeAsync(Show);

            }
            else {
                string pathEncrypted = MContent.Archivo;
                byte[] pathEncryptedByte = System.Convert.FromBase64String(pathEncrypted);
                setKeyIV();
                string pathDecrypted = encrypt_service.Decrypt(pathEncryptedByte);
                encrypt_service.EncryptFile(pathDecrypted);
                await OnClose.InvokeAsync(Show);
            }
        }
        string getSrc() {
            //string src = MContent.Archivo; 
            //AQUI HAY QUE DESENCRIPTAR EL ARCHIVO Y EL PATH PARA HACERLO DINAMICO
            string pathEncrypted = MContent.Archivo;
            byte[] pathEncryptedByte = System.Convert.FromBase64String(pathEncrypted);
            setKeyIV();
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
            setKeyIV();
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
            setKeyIV();
            string pathDecrypted = encrypt_service.Decrypt(pathEncryptedByte);
            string filename = MContent.Nombre;
            encrypt_service.DecryptFile(pathDecrypted);
            string path = pathDecrypted.Replace("wwwroot/", "");
            return path;
        }
        string getVideo() {
            string pathEncrypted = MContent.Archivo;
            byte[] pathEncryptedByte = System.Convert.FromBase64String(pathEncrypted);
            setKeyIV();
            string pathDecrypted = encrypt_service.Decrypt(pathEncryptedByte);
            string filename = MContent.Nombre;
            encrypt_service.DecryptFile(pathDecrypted);
            string path = pathDecrypted.Replace("wwwroot/", "");
            return path;
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
