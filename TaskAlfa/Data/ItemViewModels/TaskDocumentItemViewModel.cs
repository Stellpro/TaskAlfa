using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskDb.Models;

namespace TaskAlfa.Data.ItemViewModels
{
    public class TaskDocumentItemViewModel: ICloneable
    {
        private TaskDocument item { get; set; }
        public TaskDocument Item
        {
            get => item;
        }
        public TaskDocumentItemViewModel()
        {
            item = new TaskDocument();
        }

        public TaskDocumentItemViewModel(TaskDocument item)
        {
            this.item = item;
        }
        public int TaskDocumentId
        {
            get => item.TaskDocumentId;
            set => item.TaskDocumentId = value;
        }
        public int TaskId
        {
            get => item.TaskId;
            set => item.TaskId = value;
        }
        public string FileName
        {
            get => item.FileName;
            set => item.FileName = value;
        }
        public byte[] Dokument
        {
            get => item.Dokument;
            set => item.Dokument = value;
        }
        public object Clone()
        {
            var temp = item.Clone() as TaskDocument;
            return new TaskDocumentItemViewModel(temp);
        }
    }
}
