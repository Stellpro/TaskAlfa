using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskAlfa.Data.ItemViewModels;
using TaskDb;
using TaskDb.Models;

namespace TaskAlfa.Data.Services
{
    public class TaskStatusService
    {
        private TaskDbContext _context;
        private EFGenericRepository<TaskDb.Models.TaskStatus> TaskStatusRepo;
        public TaskStatusService(TaskDbContext _context)
        {
            this._context = _context;
            TaskStatusRepo = new EFGenericRepository<TaskDb.Models.TaskStatus>(_context);

        }
        public TaskStatusItemViewModel Convert(TaskDb.Models.TaskStatus model)
        {
            return new TaskStatusItemViewModel(model);
        }


       

        public TaskStatusItemViewModel Reload(TaskTable item)
        {
            var newitem = TaskStatusRepo.Reload(item.TaskStatusId);
            return Convert(newitem);
        }
        public TaskStatusItemViewModel Update(TaskStatusItemViewModel item)
        {
            var x = TaskStatusRepo.FindByIdForReload(item.TaskStatusId);
            x.StatusName = item.StatusName;
            x.OrderId = item.OrderId;
            x.IsVisible = item.IsVisible;
            x.IsDeleted = item.IsDeleted;
            return Convert(TaskStatusRepo.Update(x, item.Item.RowVersion));
        }

        //public async Task<List<TaskStatusItemViewModel>> GetFiltering(int param)
        //{
        //    var param1 = TaskStatusRepo.GetQuery();
        //    if (param > 0)
        //    {
        //        param1 = param1.Where(x => x.TaskId == param);
        //    }
        //    var result = param1.ToList();
        //    var item = result.Select(x => Convert(x)).ToList();
        //    return item;
        //}
        //public async Task<List<TaskStatusItemViewModel>> GetNameFiltering(string param)
        //{
        //    var param1 = TaskStatusRepo.GetQuery();
        //    if (param != null)
        //    {
        //        param1 = param1.Where(x => x.TaskName == param);
        //    }
        //    var result = param1.ToList();
        //    var item = result.Select(x => Convert(x)).ToList();
        //    return item;
        //}
        public TaskStatusItemViewModel Create(TaskStatusItemViewModel item)
        {


            var newitem =
            TaskStatusRepo.Create(item.Item);

            return Convert(newitem);


        }

        public TaskStatusItemViewModel UpdateIsDelete(TaskStatusItemViewModel item)
        {
            var isDelete = item.IsDeleted;
            var x = TaskStatusRepo.FindByIdForReload(item.TaskStatusId);
            if (x == null)
            {

            }
            x.IsDeleted = isDelete;
            return Convert(TaskStatusRepo.Update(x, item.Item.RowVersion));
        }

        public void Remove(TaskStatusItemViewModel item)
        {
            TaskStatusRepo.Remove(item.Item);
        }


        public List<TaskStatusItemViewModel> GetList()
        {
            List<TaskStatusItemViewModel> list = TaskStatusRepo.GetQuery().ToList().Select(x => Convert(x)).ToList();
            return list;
        }
        //public void Remove(TaskStatusItemViewModel item)
        //{
        //    TaskStatusRepo.Remove(item.Item);
        //}
    }
}
