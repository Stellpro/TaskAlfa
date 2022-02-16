using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAlfa.Reporting.Models
{
   public class TaskAlfaItemReportModel
    {
        public int Taskid { get; set; }
        public string StatusName { get; set; }
        public string TaskName { get; set; }
        public double PlanDuration { get; set; }
        public double RealDuration { get; set; }
        
    }
}
