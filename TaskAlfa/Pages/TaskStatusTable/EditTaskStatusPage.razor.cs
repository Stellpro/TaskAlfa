using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskAlfa.Data.ItemViewModels;
using TaskAlfa.PageModels.Interface;

namespace TaskAlfa.Pages.TaskStatusTable
{
    public class EditTaskStatusItemViewModel : IEditModel
    {
        public bool DialogIsOpen { get; set; }
        public bool IsConcurrencyError { get; set; }

        public List<TaskStatusItemViewModel> StatusModel { get; set; }
        public TaskStatusItemViewModel TaskStatusModel { get; set; }
        public string ErrorString { get; set; }
    }
}
