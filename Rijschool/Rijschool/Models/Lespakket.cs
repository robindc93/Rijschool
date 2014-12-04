using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rijschool.Models
{
    public class Lespakket
    {
        public int Id { get; set; }
        public string Omschrijving { get; set; }
        public int Kostprijs { get; set; }
        public int AantalBlokken { get; set; }
        [Required]
        public Rijbewijs TypeRijbewijs { get; set; }
        public virtual ICollection<Bestelling> Bestellingen { get; set; }
    }
}