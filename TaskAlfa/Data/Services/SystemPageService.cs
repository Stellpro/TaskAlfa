using System;
using System.Collections.Generic;
using System.Linq;
using DbRepository;
using AlfaControlingDb.Models;
using AlfaControlingDb;
using TaskAlfa.PagesModels.SystemPage;

namespace TaskAlfa.Data.Services
{
    public class SystemPageService
    {
        private EFRepository<LogApplicationError> appRepo { get; set; }
        private EFRepository<LogUserErrorRequest> userRepo { get; set; }
        public SystemPageService(ControllingDbContext dbContext)
        {
            this.appRepo = new EFRepository<LogApplicationError>(dbContext);
            userRepo = new EFRepository<LogUserErrorRequest>(dbContext);
        }
        public List<ErrorLogModel> GetModel(FilterErrorLogModel filter)
        {
            var endDate = filter.EndDate.AddDays(1);
            return appRepo.GetQuery().Where(r => (string.IsNullOrEmpty(filter.ErrorFltr) || r.ErrorMsg.Contains(filter.ErrorFltr))
            && (string.IsNullOrEmpty(filter.UserFltr) || r.UserData.Contains(filter.UserFltr))
            && r.InsertDate >= filter.StartDate && r.InsertDate < endDate)
                .OrderByDescending(r => r.InsertDate)
                .Select(r => new ErrorLogModel()
                {
                    Id = r.LogApplicationErrorId,                 
                    ErrorLevel = r.ErrorLevel,
                    ErrorMsg = r.ErrorMsg,
                    ErrorContext = r.ErrorContext,
                    InsertDate = r.InsertDate,
                    BrowserInfo = r.BrowserInfo,
                    AppVersion = r.AppVersion
                }).ToList();
        }
        public List<ErrorLogModel> GetModelUserMsg()
        {
            return userRepo.Get().OrderByDescending(r => r.InsertDate)
                .Select(r => new ErrorLogModel()
                {
                    Id = r.LogUserErrorRequestId,
                    ErrorLevel = r.ErrorLevel,
                    ErrorMsg = r.ErrorMsg,
                    ErrorContext = r.ErrorContext,
                    InsertDate = r.InsertDate,
                    ErrorMsgUser = r.ErrorMsgUser
                }).ToList();
        }
        public void RemoveByErrorId(int id)
        {
            var error = appRepo.FindById(id);
            appRepo.Remove(error);
        }
        public void RemoveUserMsgErrorByErrorId(int id)
        {
            var error = userRepo.FindById(id);
            userRepo.Remove(error);
        }
        public void Remove(DateTime start, DateTime? end = null)
        {
            if (end != null)
            {
                end = end?.AddDays(1);
            }
            var list = appRepo.GetQuery().Where(r => r.InsertDate >= start && (end == null || r.InsertDate < end)).ToList();
            foreach (var item in list)
            {
                appRepo.Remove(item);
            }
        }
    }
}