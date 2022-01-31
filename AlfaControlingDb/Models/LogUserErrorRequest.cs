using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlfaControlingDb.Models
{
    [Table("LogUserErrorRequest")]
    public class LogUserErrorRequest
    {
        [Key]
        public int LogUserErrorRequestId { get; set; }
        public string ErrorContext { get; set; }
        public string ErrorMsgUser { get; set; }
        public string ErrorMsg { get; set; }
        public DateTime InsertDate { get; set; }
        public string UserData { get; set; }
        public int? ErrorLevel { get; set; }
    }
}
