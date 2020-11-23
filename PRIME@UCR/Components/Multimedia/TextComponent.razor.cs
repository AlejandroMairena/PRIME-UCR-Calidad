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
        /* Function pass as parameter from Parent Component to be notified
         * when a file has been uploaded. 
         */
        [Parameter]
        public EventCallback<MultimediaContent> OnFileUpload { get; set; }
        [Parameter]
        public IEncryptionService EncryptionService { get; set; }

        string fileName; //input file name
        string text; // input file text

        // Metho to store text file
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
            mc = await mcService.AddMultimediaContent(mc); // add MC to DB
            EncryptionService.EncryptFile(path); // encrypt file
            await OnFileUpload.InvokeAsync(mc); // invoke callback function
        }

    }
}
