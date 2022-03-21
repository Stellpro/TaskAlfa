using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TaskAlfa.PageModels.Interface;
using TaskDb.Models;

namespace TaskAlfa.Data.ItemViewModels
{
    public class TaskStatusItemViewModel : ICloneable, IIsRefreshed
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
        [Required]
        public int OrderId
        {
            get => item.OrderId;
            set => item.OrderId = value;
        }
        [Required]
        public bool IsVisible
        {
            get => item.IsVisible;
            set => item.IsVisible = value;
        }
      public bool IsDeleted
        {
            get => item.IsDeleted;
            set => item.IsDeleted = value;
        }
        public bool IsRefreshed { get; set; } = false;
        public int Count
        {
            get => item.Count;
            set => item.Count = value;
        }
        
        public object Clone()
        {
            var temp = item.Clone() as TaskStatus;
            return new TaskStatusItemViewModel(temp);
        }
    }
}
