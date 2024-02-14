using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSorter.Models
{
    public abstract class AbstractModel
    {
        public abstract string Name { get; set; }
        public abstract string Path { get; set; }
        public abstract string Type { get; set; }
        public abstract string Extension { get; set; }
        public abstract long Size { get; set; }
        public abstract DateTime CreationDate { get; protected set; }
        public abstract DateTime LastModifiedDate { get; set; }
    }
}
