using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using PRIME_UCR.Application.DTOs.CheckLists;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Application.Services.CheckLists;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Application.Services.MedicalRecords;
using Microsoft.JSInterop;

namespace PRIME_UCR.Components.CheckLists
{
    public partial class PdfComponent
    {
        [Inject] private Microsoft.JSInterop.IJSRuntime JS { get; set; }
        [Inject] private IPdfService MyPdfService { get; set; }
        [Inject] private IIncidentService MyIncidentService { get; set; }
        [Inject] private IChronicConditionService MyChronicConditionService { get; set; }
        // Pasar otro parametro que sea lo que incidentes utiliza en el listado de todos los incidentes.
        [Parameter] public string IncidentCode { get; set; }

        private bool Disabled { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {

        }
        protected async Task ExportToPdf()
        {
            PdfModel pdfModel = new PdfModel();
            pdfModel.Incident = await MyIncidentService.GetIncidentDetailsAsync(IncidentCode);
            pdfModel.ChronicConditions = await MyChronicConditionService.GetChronicConditionByRecordId(pdfModel.Incident.MedicalRecord.Id);
            string fileName = "Reporte " + IncidentCode + ".pdf";
            using (MemoryStream excelStream = MyPdfService.GenerateIncidentPdf())
            {
                await JS.InvokeAsync<object>("saveAsFile", fileName, Convert.ToBase64String(excelStream.ToArray()));
            }
        }
    }
}
