using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PRIME_UCR.Application.DTOs.CheckLists;

namespace PRIME_UCR.Application.Services.CheckLists
{
    public interface IPdfService
    {
        Task GenerateIncidentPdf(PdfModel information);
    }
}
