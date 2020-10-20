using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Application.Services.CheckLists;
using PRIME_UCR.Application.Services.Multimedia;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using BlazorInputFile;
using PRIME_UCR.Domain.Models.CheckLists;
using System.Collections.Generic;

namespace PRIME_UCR.Components.CheckLists
{
    // User Story PIG01IIC20-267 LG - Agregar imagen descriptiva a lista de chequeo
    // This component allows the user to upload an image into a checklist
    public class LoadImageComponentBase : ComponentBase
    {
        protected string lastFile = "";
        protected string dropClass = "";

        protected void HandleDragEnter()
        {
            dropClass = "dropzone-drag";
        }

        protected void HandleDragLeave()
        {
            dropClass = "";
        }

        // User Story PIG01IIC20-267 LG - Agregar imagen descriptiva a lista de chequeo
        // Shares the data from a checklist with its parent component
        [Parameter]
        public EventCallback<CheckList> listChanged { get; set; }

        [Parameter]
        public CheckList list { get; set; }

        // [Inject] public IFileService file_service { get; set; }

        [Inject] public ITempFileServiceNoEncryption file_service { get; set; }

        [Inject] public IEncryptionService encrypt_service { get; set; }

        [Inject] public ICheckListService checklist_service { get; set; }

        // User Story PIG01IIC20-267 LG - Agregar imagen descriptiva a lista de chequeo
        // Updates the data from the checklist to its parent component
        protected Task OnlistChanged()
        {
            return listChanged.InvokeAsync(list);
        }

        // User Story PIG01IIC20-267 LG - Agregar imagen descriptiva a lista de chequeo
        // Handles the image uploaded by the user
        // @param files: list of files uploaded by the user, it only contains 1 .png or .jpg file
        protected async Task HandleSelectedImage(IFileListEntry[] files)
        {
            dropClass = "";

            IFileListEntry file = files.FirstOrDefault();
            lastFile = file.Name;
            //string filePath = Path.Combine(file_service.FilePath, file.Name);
            string filePath = "/images/" + file.Name;
            //byte[] pathEncrypted = encrypt_service.Encrypt(filePath);
            //string pathEncryptedString = System.Text.Encoding.UTF8.GetString(pathEncrypted);
            // stores the file (without encrypting it) in the /wwwroot/images directory)

            await file_service.StoreFile(file.Name, file.Data);
            // Saves the name of the file into the correconding checklist entry in the database
            list.ImagenDescriptiva = filePath;
            await OnlistChanged();
        }
    }
}