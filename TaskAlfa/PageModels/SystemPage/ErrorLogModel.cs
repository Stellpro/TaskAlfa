using System;

namespace TaskAlfa.PagesModels.SystemPage
{
    public class ErrorLogModel
    {
        public int Id { get; set; }
        public string ErrorContext { get; set; }
        public string ErrorMsgUser { get; set; }
        public string ErrorMsg { get; set; }
        public DateTime InsertDate { get; set; }
        public int? ErrorLevel { get; set; }
        public string BrowserInfo { get; set; }
        public string AppVersion { get; set; }
    }
}
