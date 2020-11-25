using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using PRIME_UCR.Application.Services.CheckLists;
using Microsoft.JSInterop;

namespace PRIME_UCR.Components.CheckLists
{
    public partial class PdfComponent
    {
        [Inject] private Microsoft.JSInterop.IJSRuntime JS { get; set; }
        [Inject] private IPdfService MyPdfService { get; set; }

        protected async Task ExportToPdf()
        {
            using (MemoryStream excelStream = MyPdfService.GenerateIncidentPdf())
            {
                await JS.InvokeAsync<object>("saveAsFile", "Reporte.pdf", Convert.ToBase64String(excelStream.ToArray()));
            }
        }
    }
}
