using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rijschool.Models
{
    public class Rijbewijs
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public virtual ICollection<Instructeur> Instructeurs { get; set; }
    }
}