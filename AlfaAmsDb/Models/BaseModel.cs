using System;

namespace Task.AlfaAmsDb.Models
{
    public class BaseModel
    {
        public int Version { get; set; }
        public string Bezeichnung { get; set; }
        public DateTime? InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string InsertUser { get; set; }
        public string UpdateUser { get; set; }
        public bool IsDeleted { get; set; }
    }
}
