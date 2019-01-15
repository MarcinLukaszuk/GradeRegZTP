using HiQPdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace GradeRegZTP.Builder
{
    public class PDFTimetableBuilder : ITimetableBuilder
    {
        private StringBuilder pdf = new StringBuilder();
        private bool header = false, first = true;
        public PDFTimetableBuilder()
        {
            pdf.Append("<table border=\"1\">\n");
        }
        public void AddColumn(string columnName)
        {
            if (header)
                pdf.Append("<th><p>").Append(columnName).Append("</p></th>\n");
            else
                pdf.Append("<td>").Append(columnName).Append("</td>\n");
        }

        public void AddHeader()
        {
            first = false;
            header = true;
            pdf.Append("<tr>\n");
        }

        public void AddRow()
        {
            if (!first) pdf.Append("</tr>\n");
            first = false;
            header = false;
            pdf.Append("<tr>\n");
        }

        public FileResult GeneratePDF()
        {
            if (!first)
                pdf.Append("</tr>\n");
            pdf.Append("</table>\n");

            HtmlToPdf htmlToPdfConverter = new HtmlToPdf();
            byte[] pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(pdf.ToString(), null);

            // send the PDF file to browser
            FileResult fileResult = new FileContentResult(pdfBuffer, "application/pdf");
            fileResult.FileDownloadName = "ThisMvcViewToPdf.pdf";


            return fileResult;
        }
    }
}