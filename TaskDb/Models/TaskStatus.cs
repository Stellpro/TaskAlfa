using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskDb.Models
{
    [Table("TaskStatus")]
   public class TaskStatus:ICloneable
    {
        [Key]
        [Column("TaskStatusId")]
        public int TaskStatusId { get; set; }
        public string StatusName { get; set; }
        public int OrderId { get; set; }
        public bool IsVisible { get; set; }
        public bool IsDeleted { get; set; }
        public int Count { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
