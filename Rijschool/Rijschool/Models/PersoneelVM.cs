using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace Rijschool.Models
{
    public class PersoneelVM
    {
    }

    public class PersoneelDashboardVM
    {
        public IPagedList<Klant> Klanten { get; set; }
       
        public IPagedList<Instructeur> Instructeurs { get; set; }

        public int PageInstructeur { get; set; }

        public int PageKlant { get; set; }
        
        public int AantalKlanten { get; set; }

        public int AantalInstructeurs { get; set; }
    }
}