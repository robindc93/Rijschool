using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Rijschool.Models
{
    public class Persoon : ApplicationUser
    {
        [Required]
        public string Familienaam { get; set; }
        [Required]
        public string Voornaam { get; set; }
        public string Adres { get; set; }
        public string Gemeente { get; set; }
    }

    public class Klant : Persoon
    {
        public DateTime KlantSedert { get; set; }
        public virtual ICollection<Bestelling> Bestellingen { get; set; }
    }

    public class Instructeur : Persoon
    {
        public Instructeur()
        {
            Lessen = new List<Les>();
            TypeRijbewijzen = new List<Rijbewijs>();
        }

        public virtual ICollection<Les> Lessen { get; set; }
        public virtual ICollection<Rijbewijs> TypeRijbewijzen { get; set; }

        public Boolean AddRijbewijs(Rijbewijs nieuwRijbewijs)
        {
            TypeRijbewijzen.Add(nieuwRijbewijs);
            return true;
        }

        //Deze code hoort eigenlijk thuis in een controller
        public Boolean AddRijbewijs(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Rijbewijs nieuwRijbewijs = db.TypeRijbewijzen.Find(id);
            if (nieuwRijbewijs == null)
            {
                throw new ArgumentException("Id not found");
            }

            AddRijbewijs(nieuwRijbewijs);
            return true;
        }
    }
}