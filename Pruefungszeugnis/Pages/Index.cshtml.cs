using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pruefungszeugnis.Models;
using Pruefungszeugnis.Services;
using System;

namespace Pruefungszeugnis.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel()
        {
        }

        [BindProperty]
        public PdfData PdfData { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var creator = new PDFCreator(this.PdfData);
            creator.CreatePDF();
            this.Response.Headers.Add("content-disposition", $"attachment;filename=PruefungsZeugnis_{this.PdfData.CompanyName}_{DateTime.Now.ToString("yyyyMMdd")}");
            return File(creator.GetPDF(), "application/pdf");
        }
    }
}
