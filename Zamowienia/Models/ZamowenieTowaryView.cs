using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zamowienia.Models
{
    public class ZamowenieTowaryView
    {
        public Zamowienie Zamowienie { get; set; }
        public IEnumerable<Towary> Towary { get; set; }
        public Towary nowyTowar { get; set; }
    }
}
