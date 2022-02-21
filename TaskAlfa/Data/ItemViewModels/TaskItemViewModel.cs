using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TaskAlfa.PageModels.Interface;
using TaskDb.Models;

namespace TaskAlfa.Data.ItemViewModels
{
    public class TaskItemViewModel : ICloneable, IIsRefreshed
    {
        private TaskTable item { get; set; }
        public TaskTable Item
        {
            get => item;
        }
        public TaskItemViewModel()
        {
            item = new TaskTable();
        }

        public TaskItemViewModel(TaskTable item)
        {
            this.item = item;
        }

        public int TaskId
        {
            get => item.TaskId;
        }
        [Required]
        public int TaskStatusId
        {
            get => item.TaskStatusId;
            set => item.TaskStatusId = value;
        }
        [Required]
        public string TaskName
        {
            get => item.TaskName;
            set => item.TaskName = value;
        }
        [Required]

        public double PlanDuration
        {
            get => item.PlanDuration;
            set => item.PlanDuration = value;
        }
        [Required]
        public double RealDuration
        {
            get => item.RealDuration;
            set => item.RealDuration = value;
        }
        public bool IsDeleted
        {
            get => item.IsDeleted;
            set => item.IsDeleted = value;
        }
        public string Description
        {
            get => item.Description;
            set => item.Description = value;
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
        public List<ChangeLog> ChangeLog { get; set; }
        public bool IsRefreshed { get; set; } = false;
        public object Clone()
        {
            var temp = item.Clone() as TaskTable;
            return new TaskItemViewModel(temp);
        }
    }
}
