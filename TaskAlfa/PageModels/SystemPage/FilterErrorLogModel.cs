using System;

namespace TaskAlfa.PagesModels.SystemPage
{
    public class FilterErrorLogModel
    {
        public string UserFltr { get; set; }
        public DateTime StartDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        public DateTime EndDate { get; set; } = DateTime.Now;
        public string ErrorFltr { get; set; }
    }
}
