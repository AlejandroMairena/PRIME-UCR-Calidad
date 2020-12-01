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
        [Parameter] public string IncidentCode { get; set; }
        [Parameter] public string IncidentState { get; set; }
        [Parameter] public EventCallback<bool> ChangeLoading { get; set; }

        private bool Disabled { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            // You can only generate the pdf if the incident is after the 'Asignado' state
            if (IncidentState == "Iniciado" || IncidentState == "Creado" || IncidentState == "Rechazado" || IncidentState == "Aprobado" || IncidentState == "Asignado")
            {
                Disabled = true;
            }
        }
        protected async Task ExportToPdf()
        {
            await ChangeLoading.InvokeAsync(true);
            PdfModel pdfModel = new PdfModel();
            pdfModel.Incident = await MyIncidentService.GetIncidentDetailsAsync(IncidentCode);
            pdfModel.ChronicConditions = await MyChronicConditionService.GetChronicConditionByRecordId(pdfModel.Incident.MedicalRecord.Id);
            string fileName = "Reporte " + IncidentCode + ".pdf";
            using (MemoryStream excelStream = MyPdfService.GenerateIncidentPdf())
            {
                await JS.InvokeAsync<object>("saveAsFile", fileName, Convert.ToBase64String(excelStream.ToArray()));
            }
            await ChangeLoading.InvokeAsync(false);
        }
    }
}
