using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rijschool.Models
{
    public class Bestelling
    {
        public int Id { get; set; }
        [Required]
        public virtual Klant Klant { get; set; }
        [Required]
        public Lespakket Lespakket { get; set; }
        public DateTime BestelDatum { get; set; }
        public virtual ICollection<Les> Lessen { get; set; }
        public bool Betaald { get; set; }
    }
}