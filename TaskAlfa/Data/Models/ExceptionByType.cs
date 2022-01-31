using System;

namespace TaskAlfa.Data.Models
{
    public class ExceptionByType : Exception
    {
        public ExeptionTypeEnum mExeptionType;
        public ExceptionByType(ExeptionTypeEnum exeptionType) : base(Globals.ExceptionText[exeptionType])
        {
            mExeptionType = exeptionType;
        }

        public ExceptionByType(ExeptionTypeEnum exeptionType, string message) : base(message)
        {
            mExeptionType = exeptionType;
        }
    }
}
