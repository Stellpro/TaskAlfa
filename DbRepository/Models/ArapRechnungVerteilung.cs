using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbRepository.Models
{
    [Table("ArapRechnungVerteilung")]
    public class ArapRechnungVerteilung
    {
        [Key]
        public int ArapRechnungVerteilungId { get; set; }
        public int ArapRechnungId { get; set; }
        public DateTime InsertDate { get; set; }
        public double Umsatz { get; set; }
        public char SollHaben { get; set; }
        public string BuGegenkonto { get; set; }
        public string Belegfeld1 { get; set; }
        public string Belegfeld2 { get; set; }
        public DateTime Datum { get; set; }
        public string Konto { get; set; }
        public string Kostenstelle1 { get; set; }
        public string Kostenstelle2 { get; set; }
        public string Buchungstext { get; set; }
        public char Festschreibung { get; set; }
        public DateTime? ExportDatum { get; set; }
        public Guid? ExportGuid { get; set; }
    }
}
