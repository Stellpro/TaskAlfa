using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskDb.Models
{
    
    public interface IRowVersion
    {
        public byte[] RowVersion { get; set; }
    }
}

