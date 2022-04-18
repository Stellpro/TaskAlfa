using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskAlfa.Data.ItemViewModels;
using TaskDb;
using TaskDb.Models;

namespace TaskAlfa.Data.Services
{
    public class TaskService
    {

        private TaskDbContext _context;

        private EFGenericRepository<TaskTable> TaskRepo;
        public TaskService(TaskDbContext _context)
        {
            this._context = _context;
            TaskRepo = new EFGenericRepository<TaskTable>(_context);
        }
        public TaskItemViewModel Convert(TaskTable model)
        {
            return new TaskItemViewModel (model);
        }
        public TaskItemViewModel Reload(TaskTable item)
        {
            var newitem = TaskRepo.Reload(item.TaskId);
            return Convert(newitem);
        }
        public TaskItemViewModel Update(TaskItemViewModel item)
        {
            var x = TaskRepo.FindByIdForReload(item.TaskId);
            x.TaskStatusId = item.TaskStatusId;
            x.TaskName = item.TaskName;
            x.PlanDuration = item.PlanDuration;
            x.RealDuration = item.RealDuration;
            x.Description = item.Description;
            return Convert(TaskRepo.Update(x, item.Item.RowVersion));
        }
        public TaskItemViewModel UpdateIsDelete(TaskItemViewModel item)
        {
            var isDelete = item.IsDeleted;
            var x = TaskRepo.FindByIdForReload(item.TaskId);     
            x.IsDeleted = isDelete;
            return Convert(TaskRepo.Update(x, item.Item.RowVersion));
        }
        public async Task<List<TaskItemViewModel>> GetFiltering( int param)
        {
            var param1 = TaskRepo.GetQuery();
            if (param > 0)
            {
                param1 = param1.Where(x => x.TaskId == param);
            }
            var result = param1.ToList();
            var item = result.Select(x => Convert(x)).ToList();
            return item;
        }
        public async Task<List<TaskItemViewModel>> GetNameFiltering(string param)
        {
            var param1 = TaskRepo.GetQuery();
            if (param !=null)
            {
                param1 = param1.Where(x => x.TaskName == param);
            }
            var result = param1.ToList();
            var item = result.Select(x => Convert(x)).ToList();
            return item;
        }
        public TaskItemViewModel Create(TaskItemViewModel item)
        {
            var newitem =
            TaskRepo.Create(item.Item);
            return Convert(newitem);
        }
        public List<TaskItemViewModel> GetList()
        {
            List<TaskItemViewModel> list = TaskRepo.GetQuery().ToList().Select(x => Convert(x)).ToList();
            return list;
        }
        public void Remove(TaskItemViewModel item)
        {
            TaskRepo.Remove(item.Item);
        }
    }
}
