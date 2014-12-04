using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rijschool.Models
{
    public class Les
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public DateTime UurStart { get; set; }
        public DateTime UurEinde { get; set; }
        [Required]
        public Bestelling Bestelling { get; set; }
        [Required]
        public Instructeur Instructeur { get; set; }
    }
}