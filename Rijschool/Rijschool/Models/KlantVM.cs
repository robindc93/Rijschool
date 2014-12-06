using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rijschool.Models
{
    public class KlantVM
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Voornaam")]
        public string Voornaam { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Familienaam")]
        public string Familienaam { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Straat en huisnummer")]
        public string Adres { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Gemeente")]
        public string Gemeente { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Het wachtwoord moet minstens uit {2} tekens bestaan.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Wachtwoord { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bevestig wachtwoord")]
        [Compare("Wachtwoord", ErrorMessage = "Het wachtwoord en bevestigde wachtwoord komen niet overeen.")]
        public string BevestigWachtwoord { get; set; }
    }
}