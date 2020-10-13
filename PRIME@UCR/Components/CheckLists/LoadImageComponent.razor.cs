using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Application.Services.CheckLists;
using PRIME_UCR.Application.Services.Multimedia;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using BlazorInputFile;

namespace PRIME_UCR.Components.CheckLists
{
    public class LoadImageComponentBase : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public EventCallback<CheckList> listChanged { get; set; }

        [Parameter]
        public CheckList list { get; set; }

        [Inject] public IFileService file_service { get; set; }

        [Inject] public IEncryptionService encrypt_service { get; set; }

        [Inject] public ICheckListService checklist_service { get; set; }

        protected Task OnlistChanged()
        {
            list = list;
            return listChanged.InvokeAsync(list);
        }

        protected bool open = false;
        protected string divDDClass = "dropdown";
        protected string ddMenuClass = "dropdown-menu";

        public void Open()
        {
            divDDClass = open ? "dropdown" : "dropdown show";
            ddMenuClass = open ? "dropdown-menu" : "dropdown-menu show";

            open = !open;
        }

        protected async Task HandleSelectedImage(IFileListEntry[] files)
        {
            IFileListEntry file = files.FirstOrDefault();
            string filePath = Path.Combine(file_service.FilePath, file.Name);
            //byte[] pathEncrypted = encrypt_service.Encrypt(filePath);
            //string pathEncryptedString = System.Text.Encoding.UTF8.GetString(pathEncrypted);
            list = await checklist_service.SaveImage(filePath, list);
            await file_service.StoreFile(file.Name, file.Data);
            await OnlistChanged();
        }
    }
}
