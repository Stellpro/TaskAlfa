using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAlfa.Reporting.Models;

namespace TaskAlfa.Reporting
{
    public class XtraReportEngine
    {
        public XtraReportEngine()
        {

        }
        public byte[] CreateAnschreibenEmpfehlungReport(TaskAlfaReportModel model, int docFormatId)
        {
            MemoryStream stream = new MemoryStream();
            using (var report = new XtraReport1())
            {
                var masterdata = new List<TaskAlfaReportModel>();
                masterdata.Add(model);
                report.DataSource = masterdata;

                switch (docFormatId)
                {
                    case 1:
                        report.ExportToRtf(stream);

                        break;
                    case 2:
                        report.ExportToPdf(stream);

                        break;
                    case 3:
                        var docxExportOptions = new DocxExportOptions()
                        {
                            ExportMode = DocxExportMode.SingleFile,
                            TableLayout = true,
                            KeepRowHeight = true
                        };

                        report.ExportToDocx(stream, docxExportOptions);
                        break;

                    default:
                        break;
                }


            }

            return stream.ToArray();
        }
    }

}
