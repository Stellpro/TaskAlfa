using System;

namespace TaskAlfa.Shared
{
    public class ConcurrencyExceptionModel
    {
        public bool IsOpenConcurrencyModalDialog { get; set; } = false;
        public Action Reload { get; set; }
        public string ExText { get; set; }
    }
}
