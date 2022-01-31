using DbRepository.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbRepository.Models
{
    [Table("ArapRechnung")]
    public class ArapRechnung : ICloneable, IRowVersion, IChangeLog
    {
        [Key]
        public int ArapRechnungId { get; set; }
        public DateTime Rechnungsdatum { get; set; }
        public string Rechnungsnummer { get; set; }
        public double Rechnungsbetrag { get; set; }
        public DateTime Leistungsbeginn { get; set; }
        public DateTime Leistungsende { get; set; }
        public string Buchungstext { get; set; }
        public string Aufwandskonto { get; set; }
        public int Kostenstelle { get; set; }
        public string Kommentar { get; set; }
        public bool IsDeleted { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public string ChangeLogJson { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
