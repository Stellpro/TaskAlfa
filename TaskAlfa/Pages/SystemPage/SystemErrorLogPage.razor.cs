using TaskAlfa.Data.Services;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using TaskAlfa.PagesModels.SystemPage;

namespace TaskAlfa.Pages.SystemPage
{
    public class SystemErrorLogPageViewModel : TaskAlfa.PageModels.BaseViewModel
    {
        [Inject] protected SystemPageService Service { get; set; }
        [Inject] private IMatDialogService MatDialogService { get; set; }
        protected List<ErrorLogModel> Model => Service?.GetModel(Filter);
        protected FilterErrorLogModel Filter { get; set; } = new FilterErrorLogModel();
        protected string ToEmail { get; set; }

        protected async System.Threading.Tasks.Task ExportErrors()
        {
            string FileName = $"export_{DateTime.Now.ToShortDateString()}.xlsx";
            var memory = new MemoryStream();
            IWorkbook workbook;
            workbook = new XSSFWorkbook();
            ISheet excelSheet = workbook.CreateSheet($"Errors_{DateTime.Now.ToShortDateString()}");
            IRow row = excelSheet.CreateRow(0);
            int counter = 1;
            row.CreateCell(0).SetCellValue("#");
            row.CreateCell(1).SetCellValue("Insert");
            row.CreateCell(2).SetCellValue("User");
            row.CreateCell(3).SetCellValue("Level");
            row.CreateCell(4).SetCellValue("Error");
            row.CreateCell(5).SetCellValue("Stack Trace");

            foreach (var item in Model)
            {
                row = excelSheet.CreateRow(counter);
                row.CreateCell(0).SetCellValue(item.Id);
                row.CreateCell(1).SetCellValue(item.InsertDate.ToString("dd.MM.yy hh:mm:ss"));
                row.CreateCell(3).SetCellValue(item.ErrorLevel.ToString());
                row.CreateCell(4).SetCellValue(item.ErrorMsg);
                row.CreateCell(5).SetCellValue(item.ErrorContext);
                counter++;
            }
            workbook.Write(memory);
            var fileData = memory.ToArray();

            await Js.InvokeAsync<object>(
                       "saveAsFile",
                       FileName,
                       fileData);
        }


        protected async System.Threading.Tasks.Task RemoveLogs()
        { 
            var result = await MatDialogService.AskAsync("Möchten Sie wirklich alle Einträge für diese Daten löschen?", new string[] { "Ja", "Abbrechen" });

            if (result == "Ja")
            {
                try
                {
                    foreach (var item in Model)
                    {
                        Service.RemoveByErrorId(item.Id);
                        Model.Remove(item);
                    }

                    StateHasChanged();
                }
                catch (Exception e)
                {
                    CatchException(e, "SystemErrorLogPageViewModel/RemoveLogs");
                    StateHasChanged();
                }
            }
        }

        public async System.Threading.Tasks.Task Remove(ErrorLogModel error)
        {
            var result = await MatDialogService.AskAsync("Möchten Sie den Eintrag wirklich löschen?", new string[] { "Ja", "Abbrechen" });

            if (result == "Ja")
            {
                try
                {
                    Service.RemoveByErrorId(error.Id);
                    Model.Remove(error);
                    StateHasChanged();
                }
                catch (Exception e)
                {
                    CatchException(e, "SystemErrorLogPage/Remove");
                    StateHasChanged();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Model?.Clear();
                ErrorModel = null;
            }
        }
    }
}