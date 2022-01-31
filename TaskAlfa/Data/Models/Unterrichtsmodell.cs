using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskAlfa.Data.Models
{
    public class Unterrichtsmodell
    {
        public int UnterrichtsmodellId { get; set; }
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
        public int ReihenfolgeId { get; set; }
        public string BeschreibungKurz { get; set; }
    }
}
