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


        protected override void OnInitialized()
        {
            validTypeFiles = new List<string>() { "mpeg", "pdf", "doc", "docx", "xls", "txt", "mp3", "jpg", "png", "mp4", "wmv", "avi", "text/plain" };
        }

        protected override async Task OnInitializedAsync()
        {
            await JS.InvokeAsync<bool>("hasGetUserMedia", null);
        }

        // Method to handle main button click
        // It opens and closes the dropdown.
        public void Open()
        {
            divDDClass = open ? "dropdown" : "dropdown show";
            ddMenuClass = open ? "dropdown-menu" : "dropdown-menu show";

            open = !open;
        }

        // Method to handle the file selected by the user
        // If the file has a valid type it will be stored as a 
        // MultimediaContent instance in the DB and the file stored
        // in the file repository.
        // Otherwise, it will rejected and a warning will be shown
        // to the user. 
        async Task HandleFileSelected(IFileListEntry[] files)
        {
            IFileListEntry file = files.FirstOrDefault();

            validFileType = ValidateFile(file, validTypeFiles);

            if (!validFileType) return; // archivo invalido

            MultimediaContent mcontent = await StoreMultimediaContent(file);

            await file_service.StoreFile(file.Name, file.Data);
            
            await OnFileUpload.InvokeAsync(mcontent);
        }

        // Method to store a MultimediaContent from a file
        async Task<MultimediaContent> StoreMultimediaContent(IFileListEntry file)
        {
            if (file == null) return null;

            string filePath = EncryptFilePath(file_service.FilePath, file.Name);
            MultimediaContent mcontent = FileToMultimediaContent(file, filePath);
            return await multimedia_content_service.AddMultimediaContent(mcontent);
        }

        // Method to create a MultimediaContent instance from a file and a path
        // Req: encrypted path
        MultimediaContent FileToMultimediaContent(IFileListEntry file, string path)
        {
            return new MultimediaContent
            {
                Nombre = file.Name,
                Archivo = path, 
                Descripcion = "",
                Fecha_Hora = DateTime.Now,
                Tipo = file.Type
            };
        }

        // Method to Encrypt File Path
        string EncryptFilePath(string basePath, string fileName)
        {
            string filePath = Path.Combine(basePath, fileName);
            byte[] encryptedPathB = encrypt_service.Encrypt(filePath);
            string encryptedPathS = System.Text.Encoding.UTF8.GetString(encryptedPathB);
            return encryptedPathS;
        }

        // Method to Validate File Type according to accepted types
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
            string pathEncrypted = mcontent.Archivo; //AQUI EL PATH ESTA ENCRIPTADO
            string type = mcontent.Tipo;

            //hacer query para encontrar el archivo por el ID y desencriptar el path y el archivo
            //SOLO FUNCIONA CON IMAGENES
            string nombreQuemadoImg = "practica.png";
            string pathQuemadoImg = "img/practica.png";
            string nombreQuemadoPDF = "prueba.pdf";
            string pathQuemadoPDF = "img/prueba.pdf";

            //SE LLAMA A UN METODO GENERAL QUE DIFERENCIA LAS VISTAS DE LOS TIPOS
            await JS.InvokeAsync<bool>("showMultimedia", pathQuemadoImg, nombreQuemadoImg, type);
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
