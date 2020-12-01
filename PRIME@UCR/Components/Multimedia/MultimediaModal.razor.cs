﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using PRIME_UCR.Application.Services.Multimedia;
using System.Threading;

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
        [Parameter]
        public bool ShowPDF { get; set; } = false;

        public delegate Task ModalClosed();
        public event ModalClosed OnModalClosed;

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
            string pathEncrypted = MContent.Archivo;
            byte[] pathEncryptedByte = System.Convert.FromBase64String(pathEncrypted);
            string pathDecrypted = encrypt_service.Decrypt(pathEncryptedByte);
            string filename = MContent.Nombre;
            encrypt_service.DecryptFile(pathDecrypted);
            string path = pathDecrypted.Replace("wwwroot/","");
            return path;
        }
        string getName() {
            byte[] nameEncrypted = Convert.FromBase64String(MContent.Nombre);
            string name = encrypt_service.Decrypt(nameEncrypted);
            return name + MContent.Extension;
        }
        async Task getPDF() {
            string pathEncrypted = MContent.Archivo;
            byte[] pathEncryptedByte = System.Convert.FromBase64String(pathEncrypted);
            string pathDecrypted = encrypt_service.Decrypt(pathEncryptedByte);
            string filename = MContent.Nombre;
            encrypt_service.DecryptFile(pathDecrypted);
            string path = pathDecrypted.Replace("wwwroot/", "");
            bool done = await JS.InvokeAsync<bool>("showTxt", path);
            Thread.Sleep(2000);
            encrypt_service.EncryptFile(pathDecrypted);
            
        }
        string getAudio() {
            string pathEncrypted = MContent.Archivo; //jasbdabsldjbnailñjsdnilñajndinainihbfaksljnkjan
            byte[] pathEncryptedByte = System.Convert.FromBase64String(pathEncrypted);
            string pathDecrypted = encrypt_service.Decrypt(pathEncryptedByte);//wwwroot/lasjdkjn/kasbdbha/ljasndjakjasbdkj
            encrypt_service.DecryptFile(pathDecrypted);
            string path = pathDecrypted.Replace("wwwroot/", "");
            return path;
        }
        string getVideo() {
            string pathEncrypted = MContent.Archivo;
            byte[] pathEncryptedByte = System.Convert.FromBase64String(pathEncrypted);
            string pathDecrypted = encrypt_service.Decrypt(pathEncryptedByte);
            encrypt_service.DecryptFile(pathDecrypted);
            string path = pathDecrypted.Replace("wwwroot/", "");
            return path;
        }
        


    }
}
