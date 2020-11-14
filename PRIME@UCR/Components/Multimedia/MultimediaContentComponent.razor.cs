using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
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

        // Modal Variables 
        bool showModal = false;
        bool showCamera = false;
        bool showAudio = false;
        bool showImage = false;
        bool showText = false;
        bool showVideo = false;
        MultimediaContent modalMContent = null;

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
            //leer llave
            //leer iv
            //string keyString = Configuration.GetConnectionString("Key");
            //string ivString = Configuration.GetConnectionString("IV");
            string keyString = "qXOctUgD1RQCyF6dl4IjgZLAosrLh8Dn8GCklADSmvo=";
            string ivString = "fkmYijInbe9eWQbLoWtTNQ==";
            byte[] ivByte = System.Convert.FromBase64String(ivString);
            byte[] keyByte = System.Convert.FromBase64String(keyString);
            file_service.SetKeyIV(ivByte, keyByte);
            encrypt_service.SetKeyIV(ivByte, keyByte);
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
            //string path = "datas/";
            string filePath = EncryptFilePath(file_service.FilePath, file.Name);
            //string filePath = EncryptFilePath(path, file.Name);
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
            string encryptedPathS = System.Convert.ToBase64String(encryptedPathB);
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

       
        async Task ShowPopUp(MultimediaContent mcontent) {
            //para probar, esta con una imagen y path quemadas
            string name = mcontent.Nombre; //
            string pathEncrypted = mcontent.Archivo; //AQUI EL PATH ESTA ENCRIPTADO
            string type = mcontent.Tipo;
            //SE LLAMA A UN METODO GENERAL QUE DIFERENCIA LAS VISTAS DE LOS TIPOS
            //await JS.InvokeAsync<bool>("showMultimedia", pathQuemadoImg, nombreQuemadoImg, type);
            switch (type) {
                case "image/png":
                    OpenImage(mcontent); //AQUI SE LLAMA AL ABRIR IMAGEN
                    break;
                case "application/pdf":
                    OpenText(mcontent);
                    break;
                case "audio/mpeg":
                    OpenAudio(mcontent);
                    break;
                case "video/mp4":
                    OpenVideo(mcontent);
                    break;
                case "text/plain":
                    OpenText(mcontent);
                    break;
            }
        }
        string InvalidTypeMessage()
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
        string GetButonName(MultimediaContent mcontent) {
            string name = "";
            if (mcontent.Tipo == "audio/mpeg") {
                name = "Escuchar";
            }
            else {
                name = "Ver"; 
            }
            return name; 
        }
        void OpenCamera()
        {
            showModal = true;
            showCamera = true;
            showAudio = false;
            showImage = false;
            showText = false;
            showVideo = false;
            modalMContent = null;
        }
        void OpenAudio(MultimediaContent mcontent)
        {
            showModal = true;
            showCamera = false;
            showAudio = true;
            showImage = false;
            showText = false;
            showVideo = false;
            modalMContent = mcontent;
        }
        void OpenImage(MultimediaContent mcontent)
        {
            showModal = true;
            showCamera = false;
            showAudio = false;
            showImage = true;
            showText = false;
            showVideo = false;
            modalMContent = mcontent;
        }
        void OpenText(MultimediaContent mcontent) 
        {
            showModal = true;
            showCamera = false;
            showAudio = false;
            showImage = false;
            showText = true;
            showVideo = false;
            modalMContent = mcontent;
        }
        void OpenVideo(MultimediaContent mcontent) {
            showModal = true;
            showCamera = false;
            showAudio = false;
            showImage = false;
            showText = false;
            showVideo = true;
            modalMContent = mcontent;

        }
    }
}
