using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskAlfa.Data.ItemViewModels;
using TaskDb;
using TaskDb.Models;

namespace TaskAlfa.Data.Services
{
    public class TaskDocumentService
    {
        private TaskDbContext _context;
        private EFGenericRepository<TaskDocument> TaskRepo;
        public TaskDocumentService(TaskDbContext _context)
        {
            this._context = _context;
            TaskRepo = new EFGenericRepository<TaskDocument>(_context);
        }
        public TaskDocumentItemViewModel Convert(TaskDocument model)
        {
            return new TaskDocumentItemViewModel(model);
        }
        public TaskDocumentItemViewModel Update(TaskDocumentItemViewModel item)
        {
            var x = TaskRepo.FindByIdForReload(item.TaskDocumentId);          
            x.FileName = item.FileName;
            x.Dokument = item.Dokument;
            x.Comment = item.Comment;
            return Convert(TaskRepo.Update(x, item.Item.RowVersion));
        }
        public TaskDocumentItemViewModel Create(TaskDocumentItemViewModel item)
        {
            var newitem =
            TaskRepo.Create(item.Item);
            return Convert(newitem);
        }
        public List<TaskDocumentItemViewModel> GetList()
        {
            List<TaskDocumentItemViewModel> list = TaskRepo.GetQuery().ToList().Select(x => Convert(x)).ToList();
            return list;
        }
        public void Remove(TaskDocumentItemViewModel item)
        {
            TaskRepo.Remove(item.Item);
        }
    }
}
