using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using Pruefungszeugnis.Models;
using System;
using System.Globalization;
using System.IO;

namespace Pruefungszeugnis.Services
{
    public class PDFCreator
    {
        protected readonly string _html;
        protected readonly string _text;
        protected readonly PdfData _data;
        private readonly PdfDocument document;

        public PDFCreator(PdfData data)
        {
            this._text = $"Der Düngewert beträgt: {data.PricePerTon} EUR/t";
            this._data = data;
            this.document = new PdfDocument();
        }

        public byte[] GetPDF()
        {
            using (MemoryStream memStream = new MemoryStream(100))
            {
                document.Save(memStream);
                return memStream.ToArray();
            }
        }

        public void CreatePDF()
        {
            PdfPage page = document.AddPage();
            XFont font = new XFont("Arial", 20, XFontStyle.Bold, new XPdfFontOptions(PdfFontEncoding.Unicode));

            XGraphics gfx = XGraphics.FromPdfPage(page);
            // create body text and center it
            gfx.DrawString(_text, font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormats.Center);

            XRect rect = new XRect(new XPoint(), gfx.PageSize);
            rect.Inflate(-10, -15);
            rect.Offset(0, 5);

            // create header
            gfx.DrawString(this._data.CompanyName, font, XBrushes.Black, rect, XStringFormats.TopCenter);

            // create footer
            XStringFormat format = new XStringFormat();
            font = new XFont("Arial", 8);

            format.Alignment = XStringAlignment.Near;
            format.LineAlignment = XLineAlignment.Far;
            format.Alignment = XStringAlignment.Center;

            CultureInfo culture = new CultureInfo("de-DE");
            gfx.DrawString(DateTime.Now.ToString("d", culture), font, XBrushes.Black, rect, format);
        }
    }
}
