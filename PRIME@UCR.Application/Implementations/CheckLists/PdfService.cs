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

                    // Setting the header in the pdf.
                    PdfPageTemplateElement header = SetHeader(pdfDocument, "Caja Costarricense del Seguro Social – CEACO - Unidad COV19\nNúmeros Telefónicos: 2539-1313 / 8910-6105");

                    pdfDocument.Template.Top = header;

                    // Font used for the main title.
                    PdfStandardFont titleFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 16, PdfFontStyle.Bold);

                    // Adding the main title.
                    PdfTextElement title = new PdfTextElement("PRESENTACIÓN DE PACIENTE A UNIDAD COV19", titleFont, PdfBrushes.Blue);
                    PdfLayoutResult result = title.Draw(page, new PointF(75, 0));

                    // Format to follow for everything added to PDF.
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

            // Creating a template for the header.
            PdfPageTemplateElement header = new PdfPageTemplateElement(rect);

            // Font used in the header.
            PdfStandardFont pdfFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 12, PdfFontStyle.Regular);

            // Adding the header text.
            string text = title;
            header.Graphics.DrawString(text, pdfFont, PdfBrushes.Black, new RectangleF(0, 20, doc.Pages[0].GetClientSize().Width, 50));

            return header;
        }

        private PdfGrid CreateTable(int columns, int rows, List<string> headers, List<List<string>> information)
        {
            // Margin used in the cells.
            int cellMargin = 8;
            // Font used in the table.
            PdfStandardFont contentFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);

            PdfGrid pdfGrid = new PdfGrid();
            pdfGrid.Style.CellPadding.Left = cellMargin;
            pdfGrid.Style.CellPadding.Right = cellMargin;

            // Applying built-in style to the PDF grid
            pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);

            // Adding the columns to the table
            pdfGrid.Columns.Add(columns);

            // If there are headers.
            if (headers != null)
            {
                PdfGridRow row = pdfGrid.Rows.Add();
                FillRow(row, columns, headers, true);
            }

            // For every row of information to be put in the table.
            for (int index = 0; index < rows; index++)
            {
                PdfGridRow row = pdfGrid.Rows.Add();
                FillRow(row, columns, information[index], false);
            }

            // Setting the table font.
            pdfGrid.Style.Font = contentFont;

            return pdfGrid;
        }

        private PdfGridRow FillRow(PdfGridRow row, int columns, List<string> information, bool header)
        {
            // Filling one row of the table
            for (int index = 0; index < columns; index++)
            {
                row.Cells[index].Value = information[index];
                // If its a header, center the text.
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
