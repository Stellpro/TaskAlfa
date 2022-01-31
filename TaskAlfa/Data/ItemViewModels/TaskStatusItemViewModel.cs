using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TaskDb.Models;

namespace TaskAlfa.Data.ItemViewModels
{
    public class TaskStatusItemViewModel:ICloneable
    {
        private TaskStatus item { get; set; }
        public TaskStatus Item
        {
            get => item;
        }
        public TaskStatusItemViewModel()
        {
            item = new TaskStatus();
        }

        public TaskStatusItemViewModel(TaskStatus item)
        {
            this.item = item;
        }

        public int TaskStatusId
        {
            get => item.TaskStatusId;
        }
        [Required]
        public string StatusName
        {
            get => item.StatusName;
            set => item.StatusName = value;
        }
        public object Clone()
        {
            var temp = item.Clone() as TaskStatus;
            return new TaskStatusItemViewModel(temp);
        }
    }
}
