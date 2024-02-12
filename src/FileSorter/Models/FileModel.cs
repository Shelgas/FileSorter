using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSorter.Models
{
    public class FileModel : AbstractModel
    {
        public override string Name { get; set; }
        public override string Path { get; set; }
        public override string Type { get; set; }
        public string Extension { get; set; }
        public override long Size { get; set; }
        public override DateTime CreationDate { get; protected set; }
        public override DateTime LastModifiedDate { get; set; }

        public FileModel(FileInfo fileInfo)
        {
            Name = fileInfo.Name;
            Path = fileInfo.FullName;
            Extension = fileInfo.Extension;
            Size = fileInfo.Length;
            CreationDate = fileInfo.CreationTime;
            LastModifiedDate = fileInfo.LastWriteTime;
        }


        public override string ToString()
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append(Name);
            return strBuilder.ToString();
        }

    }
}
