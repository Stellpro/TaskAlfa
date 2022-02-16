using System;
using System.Collections.Generic;
using TaskAlfa.Reporting.Models;

namespace TaskAlfa.Reporting.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = new TaskAlfaReportModel();
            model.Name = "Varname";
            model.Block = new List<TaskAlfaItemReportModel>();
            model.Block.Add(new TaskAlfaItemReportModel()
            {
                Text=@"Буря мглою, небо кроет,
Вихри Снежные крутя;",
                Title = "Зимний вечер"
            });

           
            byte[] file =
                new XtraReportEngine().CreateAnschreibenEmpfehlungReport(model,
                2);

            System.IO.File.WriteAllBytes(@"C:\Users\stellpro\Desktop\TaskAlfa.pdf", file);
        }
    }
    
}
