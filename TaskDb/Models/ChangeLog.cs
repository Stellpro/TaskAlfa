using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskDb.Models
{
    public class ChangeLog
    {
        public DateTime Date { get; set; }
        public string Operation { get; set; }
    }
}
