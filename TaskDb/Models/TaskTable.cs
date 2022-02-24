using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskDb.Models
{ 
    [Table("Task")]
  public class TaskTable: ICloneable, IRowVersion, IChangeLog
    {
       [Key]
       [Column("TaskId")]
        public int TaskId { get; set; } 
        public int TaskStatusId { get; set; }
        public string TaskName { get; set; } 
        public double PlanDuration { get; set; }
        public double RealDuration { get; set; }
        public bool IsDeleted { get; set; }
        public string Description { get; set; }
        
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public string ChangeLogJson { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
