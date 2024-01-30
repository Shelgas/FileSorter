using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSorter.Models
{
    public class FileModel
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public string FileExtension { get; set; }
        public long FileSize { get; set; }
        public DateTime CreationDate { get; private set; }
        public DateTime LastModifiedDate { get; set; }

        public FileModel(FileInfo fileInfo)
        {
            FileName = fileInfo.Name;
            FilePath = fileInfo.FullName;
            FileExtension = fileInfo.Extension;
            FileSize = fileInfo.Length;
            CreationDate = fileInfo.CreationTime;
            LastModifiedDate = fileInfo.LastWriteTime;
        }

    }
}
