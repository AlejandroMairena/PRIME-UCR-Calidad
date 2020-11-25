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
        public PdfService()
        {
        }

        public MemoryStream GenerateIncidentPdf(/*PdfModel information*/)
        {
            if (/*information.Incident != null*/true) 
            {
                using (PdfDocument pdfDocument = new PdfDocument()) 
                {
                    int paragraphAfterSpacing = 8;

                    // Add page to the PDF document
                    PdfPage page = pdfDocument.Pages.Add();

                    // Create a new font
                    PdfStandardFont titleFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 16, PdfFontStyle.Bold);

                    // Create a header and draw the image.
                    PdfPageTemplateElement header = SetHeader(pdfDocument, "Caja Costarricense del Seguro Social – CEACO - Unidad COV19\nNúmeros Telefónicos: 2539-1313 / 8910-6105");

                    pdfDocument.Template.Top = header;

                    // Create a text element to draw a text in PDF page
                    PdfTextElement title = new PdfTextElement("PRESENTACIÓN DE PACIENTE A UNIDAD COV19", titleFont, PdfBrushes.Blue);
                    PdfLayoutResult result = title.Draw(page, new PointF(75, 0));
                    PdfLayoutFormat format = new PdfLayoutFormat();
                    format.Layout = PdfLayoutType.Paginate;


                    AddGeneralInformation(pdfDocument, result, page, format, paragraphAfterSpacing);


                    using (MemoryStream stream = new MemoryStream())
                    {
                        // Saving the PDF document into the stream
                        pdfDocument.Save(stream);
                        // Closing the PDF document
                        pdfDocument.Close(true);
                        return stream;

                    }
                }
            } else
            {
                return null;
            }
        }

        public PdfPageTemplateElement SetHeader(PdfDocument doc, string title)
        {
            RectangleF rect = new RectangleF(0, 0, doc.Pages[0].GetClientSize().Width, 50);

            // Create a page template
            PdfPageTemplateElement header = new PdfPageTemplateElement(rect);

            PdfStandardFont pdfFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 12, PdfFontStyle.Regular);

            string text = title;
            header.Graphics.DrawString(text, pdfFont, PdfBrushes.Black, new RectangleF(0, 20, doc.Pages[0].GetClientSize().Width, 50));

            return header;
        }

        private PdfGrid CreateTable(int columns, int rows, List<string> headers, List<List<string>> information)
        {
            int cellMargin = 8;
            PdfStandardFont contentFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);

            PdfGrid pdfGrid = new PdfGrid();
            pdfGrid.Style.CellPadding.Left = cellMargin;
            pdfGrid.Style.CellPadding.Right = cellMargin;

            // Applying built-in style to the PDF grid
            pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);

            pdfGrid.Columns.Add(columns);

            if (headers != null)
            {
                PdfGridRow row = pdfGrid.Rows.Add();
                FillRow(row, columns, headers, true);
            }

            for (int index = 0; index < rows; index++)
            {
                PdfGridRow row = pdfGrid.Rows.Add();
                FillRow(row, columns, information[index], false);
            }

            pdfGrid.Style.Font = contentFont;

            return pdfGrid;
        }

        private PdfGridRow FillRow(PdfGridRow row, int columns, List<string> information, bool header)
        {
            for (int index = 0; index < columns; index++)
            {
                row.Cells[index].Value = information[index];
                if (header)
                {
                    row.Cells[index].StringFormat.Alignment = PdfTextAlignment.Center;
                }
            }
            return row;
        }

        private void AddGeneralInformation(PdfDocument doc, PdfLayoutResult result, PdfPage page, PdfLayoutFormat format, int paragraphAfterSpacing)
        {
            PdfStandardFont contentTitleFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 12, PdfFontStyle.Bold);
            PdfTextElement content = new PdfTextElement("INFORMACIÓN GENERAL", contentTitleFont, PdfBrushes.Black);

            result = content.Draw(page, new RectangleF(175, result.Bounds.Bottom + paragraphAfterSpacing, page.GetClientSize().Width, page.GetClientSize().Height), format);

            PdfTextElement medicData = new PdfTextElement("Datos del Médico", contentTitleFont, PdfBrushes.Black);
            result = medicData.Draw(page, new RectangleF(0, result.Bounds.Bottom + paragraphAfterSpacing, page.GetClientSize().Width, page.GetClientSize().Height), format);

            List<string> headers = new List<string>();
            headers.Add("Nombre Completo");
            headers.Add("Número telefónico");
            headers.Add("Lugar de donde llama");

            List<List<string>> information = new List<List<string>>();
            List<string> rowInformation = new List<string>();
            rowInformation.Add("Temp");
            rowInformation.Add("Temp");
            rowInformation.Add("Temp");
            information.Add(rowInformation);

            PdfGrid pdfGrid = CreateTable(3, 1, headers, information);

            result = pdfGrid.Draw(page, new PointF(0, result.Bounds.Bottom + paragraphAfterSpacing));

            PdfTextElement patientData = new PdfTextElement("Datos del Paciente", contentTitleFont, PdfBrushes.Black);
            result = patientData.Draw(page, new RectangleF(0, result.Bounds.Bottom + paragraphAfterSpacing, page.GetClientSize().Width, page.GetClientSize().Height), format);

            headers = new List<string>();
            headers.Add("Nombre Completo");
            headers.Add("ID del Paciente");
            headers.Add("Fecha de Nacimiento");
            headers.Add("Edad");

            information = new List<List<string>>();
            rowInformation = new List<string>();
            rowInformation.Add("Temp");
            rowInformation.Add("Temp");
            rowInformation.Add("Temp");
            rowInformation.Add("Temp");
            information.Add(rowInformation);

            pdfGrid = CreateTable(4, 1, headers, information);

            result = pdfGrid.Draw(page, new PointF(0, result.Bounds.Bottom + paragraphAfterSpacing));
        }
    }
}
