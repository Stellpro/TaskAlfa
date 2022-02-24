using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskAlfa.Shared
{
    public class UpdateOrCreateDilogModel
    {
        public bool IsOpenUpdateOrCreate { get; set; } = false;
        public string Text { get; set; } = "Do you want to overwrite the file or create";
        
    }
}
