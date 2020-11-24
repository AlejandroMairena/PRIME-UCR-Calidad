using System;
using System.Collections.Generic;
using System.Text;
using PRIME_UCR.Application.Services.CheckLists;
using PRIME_UCR.Application.DTOs.CheckLists;
using System.Threading.Tasks;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Drawing;
using System.IO;
using Syncfusion.Pdf.Tables;

namespace PRIME_UCR.Application.Implementations.CheckLists
{
    public class PdfService : IPdfService
    {
        private PdfStandardFont font;
        private int paragraphAfterSpacing;
        PdfLayoutFormat format;

        public PdfService()
        {
            font = new PdfStandardFont(PdfFontFamily.TimesRoman, 16);
            paragraphAfterSpacing = 8;
            format = new PdfLayoutFormat();
        }

        public async Task GenerateIncidentPdf(PdfModel information)
        {
            if (information.Incident != null) 
            {
                using (PdfDocument pdfDocument = new PdfDocument()) 
                {
                    PdfPage page = pdfDocument.Pages.Add();

                    // Fonts
                    PdfStandardFont titleFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 20);

                    // Table format
                    format.Layout = PdfLayoutType.Paginate;

                    // Header
                    SetHeader(pdfDocument);

                    // Set the page title
                    PdfTextElement title = new PdfTextElement("PRESENTANCIÓN DE PACIENTE A UNIDAD COV19", titleFont, PdfBrushes.Black);
                    PdfLayoutResult result = title.Draw(page, new PointF(0, 0));

                    // General Information
                    SetGeneralInformation(page, result);

                    // Covid Unit Transfer Unit
                    SetCovidUnitInformation(page, result);
                }
            }
        }

        public void SetHeader(PdfDocument document)
        {
            // Set the header.
            RectangleF bounds = new RectangleF(0, 0, document.Pages[0].GetClientSize().Width, 50);
            PdfPageTemplateElement header = new PdfPageTemplateElement(bounds);
            // Load the header image.
            FileStream imageStream = new FileStream("Logo.png", FileMode.Open, FileAccess.Read);
            PdfImage image = new PdfBitmap(imageStream);
            header.Graphics.DrawImage(image, new PointF(0, 0), new SizeF(100, 50));
            document.Template.Top = header;
        }

        public void SetGeneralInformation(PdfPage page, PdfLayoutResult result)
        {
            PdfTextElement generalInformation = new PdfTextElement("INFORMACIÓN GENERAL", font, PdfBrushes.Black);
            result = generalInformation.Draw(page, new PointF(0, result.Bounds.Bottom + paragraphAfterSpacing));

            // Information about the doctor
            PdfTextElement doctorString = new PdfTextElement("Datos del Médico", font, PdfBrushes.Black);
            result = doctorString.Draw(page, new RectangleF(0, result.Bounds.Bottom + paragraphAfterSpacing, page.GetClientSize().Width, page.GetClientSize().Height), format);
            PdfGrid pdfGrid = new PdfGrid();
            List<object> data = new List<object>();
            Object grid1row1 = new { NombreCompleto = " ", NúmeroTelefónico = " ", LugarDeDondeLlama = " " };
            data.Add(grid1row1);
            IEnumerable<object> dataTable = data;
            pdfGrid.DataSource = dataTable;
            PdfGridLayoutResult pdfGridLayoutResult = pdfGrid.Draw(page, new PointF(10, 10));

            // Information about the patient
            PdfTextElement patientString = new PdfTextElement("Datos del Paciente", font, PdfBrushes.Black);
            result = patientString.Draw(page, new RectangleF(0, result.Bounds.Bottom + paragraphAfterSpacing, page.GetClientSize().Width, page.GetClientSize().Height), format);
            pdfGrid = new PdfGrid();
            data = new List<object>();
            Object grid2row1 = new { NombreCompleto = " ", IdDelPaciente = " ", FechaNacimiento = " ", Edad = " " };
            data.Add(grid1row1);
            dataTable = data;
            pdfGrid.DataSource = dataTable;
            pdfGridLayoutResult = pdfGrid.Draw(page, new PointF(10, pdfGridLayoutResult.Bounds.Bottom + 20));
        }

        public void SetCovidUnitInformation(PdfPage page, PdfLayoutResult result)
        {
            PdfTextElement covidNote = new PdfTextElement("NOTA TRASLADO UNIDAD COVID - NOTA MÉDICA", font, PdfBrushes.Black);
            result = covidNote.Draw(page, new PointF(0, result.Bounds.Bottom + paragraphAfterSpacing));
        }
    }
}
