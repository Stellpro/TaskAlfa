using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskDb.Models
{
    [Table("TaskDocument")]
    public class TaskDocument : ICloneable
    {
        [Key]
        [Column("TaskDocumentId")]
        public int TaskDocumentId { get; set; }
        public int TaskId { get; set; }
        public string FileName { get; set; }
        public string Comment { get; set; }
        public byte[] Dokument { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
