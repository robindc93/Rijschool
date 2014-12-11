using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rijschool.Models
{
    public class PersoneelVM
    {
    }

    public class PersoneelDashboardVM
    {
        public ICollection<Klant> Klanten { get; set; }
        public ICollection<Instructeur> Instructeurs { get; set; }

        public int AantalKlanten { get; set; }

        public int AantalInstructeurs { get; set; }
    }
}