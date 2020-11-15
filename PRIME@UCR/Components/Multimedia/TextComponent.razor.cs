using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services.Multimedia;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Multimedia
{
    public partial class TextComponent
    {
        [Inject]
        protected IFileService fileService { get; set; }
        [Inject]
        protected IMultimediaContentService mcService { get; set; }
        [Parameter]
        public EventCallback<MultimediaContent> OnFileUpload { get; set; }
        [Parameter]
        public IEncryptionService EncryptionService { get; set; }

        string fileName;
        string text;

        public async Task StoreTextFile()
        {
            fileName += ".txt";
            string path = await fileService.StoreTextFile(text, fileName);
            byte[] epArray = EncryptionService.Encrypt(path);
            string encryptedPath = Convert.ToBase64String(epArray);
            MultimediaContent mc = new MultimediaContent
            {
                Nombre = fileName,
                Archivo = encryptedPath,
                Descripcion = "",
                Fecha_Hora = DateTime.Now,
                Tipo = "text/plain"
            };
            mc = await mcService.AddMultimediaContent(mc);
            EncryptionService.EncryptFile(path);
            await OnFileUpload.InvokeAsync(mc);
        }

    }
}
