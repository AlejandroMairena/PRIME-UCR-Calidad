using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Multimedia
{
    public partial class MultimediaContentComponent
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        [Parameter]
        public List<MultimediaContent> MultimediaContent { get; set; }
        [Parameter]
        public EventCallback<MultimediaContent> OnFileUpload { get; set; }

        public List<string> validTypeFiles;
        bool validFileType = true;

        bool open = false;
        string divDDClass = "dropdown";
        string ddMenuClass = "dropdown-menu";
        string invalidMessage = "";

        // real time multimedia
        bool showCamera = false;
        string videoId = "video120";
        ElementReference videoElement;

        protected override void OnInitialized()
        {
            validTypeFiles = new List<string>() { "mpeg", "pdf", "doc", "docx", "xls", "txt", "mp3", "jpg", "png", "mp4", "wmv", "avi", "text/plain" };
        }

        protected override async Task OnInitializedAsync()
        {
            await JS.InvokeAsync<bool>("hasGetUserMedia", null);
        }

        public void Open()
        {
            divDDClass = open ? "dropdown" : "dropdown show";
            ddMenuClass = open ? "dropdown-menu" : "dropdown-menu show";

            open = !open;
        }

        IFileListEntry file;

        async Task HandleFileSelected(IFileListEntry[] files)
        {
            file = files.FirstOrDefault();

            validFileType = ValidateFile(file, validTypeFiles);

            if (!validFileType) return; // archivo invalido

            string filePath = Path.Combine(file_service.FilePath, file.Name);
            byte[] pathEncrypted = encrypt_service.Encrypt(filePath);
            string pathEncryptedString = System.Text.Encoding.UTF8.GetString(pathEncrypted);
            if (file == null) return;
            MultimediaContent mcontent = new MultimediaContent
            {
                Nombre = file.Name,
                Archivo = pathEncryptedString,
                Descripcion = "",
                Fecha_Hora = DateTime.Now,
                Tipo = file.Type
            };
            mcontent = await multimedia_content_service.AddMultimediaContent(mcontent);
            await file_service.StoreFile(file.Name, file.Data);
            //encrypt_service.EncryptFile(file_service.FilePath,file.Name);
            await OnFileUpload.InvokeAsync(mcontent);
        }

        bool ValidateFile(IFileListEntry file, List<string> validFileTypes)
        {
            if (file == null) return false;

            string type = file.Type;

            foreach (string validType in validTypeFiles)
                if (type.Contains(validType) || type == validType) return true;

            return false;
        }

        async Task OpenCamera()
        {
            showCamera = !showCamera;
            if (showCamera)
                await JS.InvokeAsync<bool>("openCamera", new object[1] { videoElement });
        }
        async Task ShowPopUp(MultimediaContent mcontent) {
            //para probar, esta con una imagen y path quemadas
            string name = mcontent.Nombre; //
          
            //hacer query para encontrar el archivo por el ID y desencriptar el path y el archivo
            //SOLO FUNCIONA CON IMAGENES
            string nombreQuemado = "practica.png";
            string pathQuemado = "img/practica.png";
            await JS.InvokeAsync<bool>("showImage", pathQuemado, nombreQuemado);
        }
        async Task CloseImageView() {
            await JS.InvokeAsync<bool>("closeView");
        }

        string invalidType()
        {
            string datas = "El archivo seleccionado no se encuentra dentro de los archivos válidos. Por favor seleccione un archivo con las siguientes extensiones: ";
            for (int i = 0; i < validTypeFiles.Count(); ++i)
            {
                datas += validTypeFiles[i];
                if (i < (validTypeFiles.Count() - 1))
                {
                    datas += ",";
                }
            }
            return datas;
        }
        string getButonName(MultimediaContent mcontent) {
            string name = "";
            if (mcontent.Tipo == "audio/mpeg") {
                name = "Escuchar";
            }
            else {
                name = "Ver"; 
            }
            return name; 
        }

    }
}
