using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using TaskAlfa.Data.ItemViewModels;

namespace TaskAlfa.Pages.Task
{
    public class AddFileView:ComponentBase
    {
        [Parameter]
        public EventCallback dialogIsOpen { get; set; }
        [Parameter]
        public TaskDocumentItemViewModel AddModel { get; set; }

        public bool IsOpen = true;
        [Parameter]
        public EventCallback<TaskDocumentItemViewModel> SaveItem { get; set; }
        public List<IBrowserFile> loadedFiles = new();
        public long maxFileSize = 1920 * 1080;
        public int maxAllowedFiles = 1;
        public bool isLoading;
        public TaskDocumentItemViewModel UpLoadList = new TaskDocumentItemViewModel();

        public async System.Threading.Tasks.Task LoadFiles(InputFileChangeEventArgs e)
        {
            isLoading = true;
            loadedFiles.Clear();
            try
            {
                var file = e.GetMultipleFiles(maxAllowedFiles).FirstOrDefault();
                loadedFiles.Add(file);
                await using MemoryStream fs = new();
                await file.OpenReadStream(maxFileSize).CopyToAsync(fs);
                var Mem = fs.ToArray();
                AddModel.Dokument = Mem;
                AddModel.FileName = file.Name;
                UpLoadList = AddModel;
            }
            catch (Exception ex)
            {
                var temp = ex.Message;
            }
            isLoading = false;          
        }
        public void Close()
        {           
            dialogIsOpen.InvokeAsync();
        }

    }
}
