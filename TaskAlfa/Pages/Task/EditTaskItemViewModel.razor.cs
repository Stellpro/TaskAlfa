using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskAlfa.Data.ItemViewModels;
using TaskAlfa.PageModels.Interface;

namespace TaskAlfa.Pages.Task
{
    public class EditTaskItemViewModel: IEditModel
    {
        public bool DialogIsOpen { get; set; }
        public bool IsConcurrencyError { get; set; }
        public TaskItemViewModel Model { get; set; }
        public List<TaskStatusItemViewModel> StatusModel { get; set; }
        public TaskStatusItemViewModel TaskStatusIdModel { get; set; }
        public string ErrorString { get; set; }
    }
}
