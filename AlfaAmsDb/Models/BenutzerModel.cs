using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlfaAmsDb.Models
{
    [Table("Benutzer")]
    public class BenutzerModel : ICloneable//, IEntity
    {
        [Key]
        public int BenutzerId { get; set; }
        public string Login { get; set; }
        public string Passwort { get; set; }
        public int? BenutzerrolleId { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
        public int AlfaStandortNr { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string InsertUser { get; set; }
        public string UpdateUser { get; set; }
        public bool IsDeleted { get; set; }
        public string Telefon { get; set; }
        public string Position { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
        public Guid? ResetToken { get; set; }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
