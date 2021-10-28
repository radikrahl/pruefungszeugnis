using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;

namespace Pruefungszeugnis.Models
{
    public class PdfData
    {
        private const float pricePerTon = 0.8F;

        [StringLength(60, MinimumLength = 3, ErrorMessage = "Bitte geben Sie mindestens 3 Buchstaben ein.")]
        [Required(ErrorMessage = "Bitte geben Sie ihren Namen an.")]
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the Azotic value.
        /// </summary>
        /// <remarks>
        /// Declare as nullable, since default value in forms is 0 if empty.
        /// </remarks>
        [Required(ErrorMessage = "Bitte geben Sie den Stickstoff wert an.")]
        [Range(1,9999, ErrorMessage = "Bitte geben Sie den Stickstoff wert an.")]
        public double? Azotic { get; set; }

        /// <summary>
        /// Gets or sets the Potassium value.
        /// </summary>
        /// <remarks>
        /// Declare as nullable, since default value in forms is 0 if empty.
        /// </remarks>
        [Required(ErrorMessage = "Bitte geben Sie den Kalium wert an.")]
        [Range(1, 9999, ErrorMessage = "Bitte geben Sie den Kalium wert an.")]
        public double? Potassium { get; set; }

        public double PricePerTon => 
            Math.Round((this.Potassium.GetValueOrDefault() + this.Azotic.GetValueOrDefault()) * pricePerTon, 4);
    }
}
