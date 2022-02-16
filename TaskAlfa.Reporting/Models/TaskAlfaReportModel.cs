using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAlfa.Reporting.Models
{
   public class TaskAlfaReportModel
    {
        public string Name { get; set; }
        public List<TaskAlfaItemReportModel> Block { get; set; }
    }
}
