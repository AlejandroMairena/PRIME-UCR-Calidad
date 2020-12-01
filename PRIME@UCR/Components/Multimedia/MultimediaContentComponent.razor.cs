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
        // The List of Multimedia Content displayed by the component
        [Parameter]
        public List<MultimediaContent> MultimediaContent { get; set; }
        /* Function pass as parameter from Parent Component to be notified
         * when a file has been uploaded. 
         */
        [Parameter]
        public EventCallback<MultimediaContent> OnFileUpload { get; set; }
        [Parameter]
        public string IncidentCode { get; set; }
        [Parameter]
        public string ActionName { get; set; }
        [Parameter]
        public bool CallingFromAction { get; set; }
        [Parameter]
        public string CheckListName { get; set; }
        [Parameter]
        public string CheckListItemName { get; set; }


        // List of valid file types 
        public List<string> validTypeFiles;
        bool validFileType = true;

        bool open = false; // State of the dropdown 
        string divDDClass = "dropdown";
        string ddMenuClass = "dropdown-menu dropdown-menu-right";
        string invalidMessage = "";

        // Modal Variables 
        bool showModal = false;
        bool showCamera = false;
        bool showMicrophone = false;
        bool showAudio = false;
        bool showImage = false;
        bool showText = false;
        bool showVideo = false;
        bool showVideoComponent = false;
        bool showTextComponent = false;
        bool showPDF = false;

        bool showDropdown = false;
        // MultimediaContent pass to the MultimediaModal component
        MultimediaContent modalMContent = null;
         
        protected override void OnInitialized()
        {
            // initialization of valid file types
            validTypeFiles = new List<string>() { "ogg", "oga", "jpeg", "webm", "mpeg", "pdf", "doc", "docx", "xls", "txt", "mp3", "jpg", "png", "mp4", "wmv", "avi", "text/plain" };
        }

        // Method to handle main button click
        // It opens and closes the dropdown.
        public void Open()
        {
            divDDClass = open ? "dropdown" : "dropdown show";
            ddMenuClass = open ? "dropdown-menu dropdown-menu-right" : "dropdown-menu dropdown-menu-right show";

            open = !open;
        }
        string encodeString(string base64String) {
            string resp = base64String.Replace("/","-");
            return resp;
        }
        string decodeString(string encodedString)
        {
            string resp = encodedString.Replace("-", "/");
            return resp;
        }
        string getFilePath() {
            string path = "wwwroot/";
            if (CallingFromAction)
            {
                byte[] IncidentCodeEncryptedByte = encrypt_service.Encrypt(IncidentCode);
                string IncidentCodeEncryptedString = Convert.ToBase64String(IncidentCodeEncryptedByte);
                if (IncidentCodeEncryptedString.Contains("/")) {
                    path += encodeString(IncidentCodeEncryptedString);
                }
                else {
                    path += IncidentCodeEncryptedString;
                }
                path += "/";
                byte[] ActionNameEncryptedByte = encrypt_service.Encrypt(ActionName);
                string ActionNameEncryptedString = Convert.ToBase64String(ActionNameEncryptedByte);
                if (ActionNameEncryptedString.Contains("/")) {
                    path += encodeString(ActionNameEncryptedString);
                }
                else{
                    path += ActionNameEncryptedString;
                }
                path += "/";
            }
            else { 
                //esta llamando listas de chequeo
            }
            return path;
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

            validFileType = ValidateFile(file);

            if (!validFileType) return; // archivo invalido

            MultimediaContent mcontent = await StoreMultimediaContent(file);
            await file_service.StoreFile(getFilePath(), mcontent.Nombre,mcontent.Extension, file.Data);
            await OnFileUpload.InvokeAsync(mcontent);
        }
        string getType(string filename) {
            string extension = ".";
            foreach (string type in validTypeFiles) {
                if (filename.Contains(type)) {
                    extension += type;
                }
            }
            return extension;
        }
        // Method to store a MultimediaContent from a file
        async Task<MultimediaContent> StoreMultimediaContent(IFileListEntry file)
        {
            if (file == null) return null;
            MultimediaContent mcontent = FileToMultimediaContent(file, getFilePath());
            return await multimedia_content_service.AddMultimediaContent(mcontent);
        }

        // Method to create a MultimediaContent instance from a file and a path
        // Req: encrypted path
        MultimediaContent FileToMultimediaContent(IFileListEntry file, string pathDecrypted)
        {
            string filename = "";
            string path = "";
            string justName = file.Name.Replace(getType(file.Name),"");
            byte[] fileNameEncrytedByte = encrypt_service.Encrypt(justName);
            string fileNameEncrytedString = Convert.ToBase64String(fileNameEncrytedByte);
            if (fileNameEncrytedString.Contains("/")) {
                filename = encodeString(fileNameEncrytedString);
            }
            else{
                filename = fileNameEncrytedString;
            }
            path = pathDecrypted + filename;
            byte[] pathEncryptedByte = encrypt_service.Encrypt(path);
            string pathEncryptedString = Convert.ToBase64String(pathEncryptedByte);

            return new MultimediaContent
            {
                Nombre = filename,
                Archivo = path,
                Descripcion = "",
                Fecha_Hora = DateTime.Now,
                Tipo = file.Type,
                Extension = getType(file.Name)
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
        bool ValidateFile(IFileListEntry file)
        {
            if (file == null) return false;

            string type = file.Type;

            foreach (string validType in validTypeFiles)
            {
                bool b1 = type.Contains(validType);
                bool b2 = type == validType;
                if (b1 || b2) return true;
            }

            return false;
        }
        string getDecryptedName(string nameEncrypted, string extension) {
            byte[] nameEncryptedByte = Convert.FromBase64String(nameEncrypted);
            string nameDecryptedString = encrypt_service.Decrypt(nameEncryptedByte);
            string name = nameDecryptedString + extension;
            return name;
        }
       
        async Task ShowPopUp(MultimediaContent mcontent) {
            string name = mcontent.Nombre; 
            string pathEncrypted = mcontent.Archivo;
            string type = mcontent.Tipo;
            if (type == "image/png")
                OpenImage(mcontent);
            else if (type == "text/plain")
                OpenText(mcontent);
            else if (type == "video/mp4" || type == "video/webm")
                OpenVideo(mcontent);
            else if (type == "audio/mpeg" || type == "audio/ogg")
                OpenAudio(mcontent);
            else if (type == "application/pdf")
                OpenPDF(mcontent);
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
            if (mcontent.Tipo == "audio/mpeg" || mcontent.Tipo == "audio/ogg") {
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
            showMicrophone = false;
            showText = false;
            showVideo = false;
            modalMContent = null;
            showVideo = false;
            showVideoComponent = false;
            showTextComponent = false;
        }
        void OpenAudio(MultimediaContent mcontent)
        {
            showModal = true;
            showCamera = false;
            showAudio = true;
            showImage = false;
            showMicrophone = false;
            showText = false;
            showVideo = false;
            modalMContent = mcontent;
            showVideoComponent = false;
        }
        void OpenImage(MultimediaContent mcontent)
        {
            showModal = true;
            showCamera = false;
            showAudio = false;
            showImage = true;
            showMicrophone = false;
            showText = false;
            showVideo = false;
            modalMContent = mcontent;
            showVideoComponent = false;
            showTextComponent = false;
            showPDF = false;
        }
        void OpenText(MultimediaContent mcontent) 
        {
            showModal = true;
            showCamera = false;
            showAudio = false;
            showImage = false;
            showMicrophone = false;
            showText = true;
            showVideo = false;
            modalMContent = mcontent;
            showVideoComponent = false;
            //showTextComponent = false;
            showPDF = false;
        }
        void OpenVideo(MultimediaContent mcontent) {
            showModal = true;
            showCamera = false;
            showAudio = false;
            showImage = false;
            showMicrophone = false;
            showText = false;
            showVideo = true;
            modalMContent = mcontent;
            showVideoComponent = false;
            showTextComponent = false;
            showPDF = false;
        }


        void OpenVideo()
        {
            showModal = true;
            showCamera = false;
            showAudio = false;
            showImage = false;
            showMicrophone = false;
            showText = false;
            showVideo = false;
            modalMContent = null;
            showVideoComponent = true;
            showTextComponent = false;
            showPDF = false;
        }
        void OpenMicrophone()
        {
            showModal = true;
            showCamera = false;
            showAudio = false;
            showImage = false;
            showMicrophone = true;
            showText = false;
            showVideo = false;
            modalMContent = null;
            showVideoComponent = false;
            showTextComponent = false;
            showPDF = false;
        }
        void OpenPDF(MultimediaContent mcontent) {
            showModal = false;
            showCamera = false;
            showAudio = false;
            showImage = false;
            showMicrophone = false;
            showText = false;
            showVideo = false;
            modalMContent = mcontent;
            showVideoComponent = false;
            showTextComponent = false;
            showPDF = true;
        }
        //void OpenTextComponent()
        //{
        //    showModal = true;
        //    showCamera = false;
        //    showAudio = false;
        //    showImage = false;
        //    showMicrophone = false;
        //    showText = false;
        //    showVideo = false;
        //    modalMContent = null;
        //    showVideo = false;
        //    showVideoComponent = false;
        //    showTextComponent = true;
        //}
        void CloseAllViews() {
            showModal = false;
            showPDF = false;
        }
        async Task DeleteMultimediaContent(MultimediaContent mcontent)
        {
            CloseAllViews();
            await multimedia_content_service.DeleteMultimediaContent(mcontent);
            MultimediaContent.Remove(mcontent);
            byte[] bEPath = Convert.FromBase64String(mcontent.Archivo);
            string path = encrypt_service.Decrypt(bEPath);
            file_service.DeleteFile(path);
        }


    }
}
