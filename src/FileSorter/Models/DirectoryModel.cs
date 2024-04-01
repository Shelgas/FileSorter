using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSorter.Models
{
    public class DirectoryModel : AbstractModel
    {
        public override string Name { get; set; }
        public override string Path { get; set; }
        public override string Type { get; set; }
        public override string Extension { get; set; }
        public override long Size { get; set; }
        public override DateTime CreationDate { get; protected set; }
        public override DateTime LastModifiedDate { get; set; }
        public List<AbstractModel> Files { get; set; }


        public DirectoryModel(DirectoryInfo directoryInfo)
        {
            Name = directoryInfo.Name;
            Path = directoryInfo.FullName;
            Extension = directoryInfo.Extension;
            CreationDate = directoryInfo.CreationTime;
            LastModifiedDate = directoryInfo.LastWriteTime;
            Type = "Directory";
            Files = new List<AbstractModel>();
        }


        private static long DirSize(DirectoryInfo d)
        {
            long size = 0;
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }

        public override string ToString()
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append(Name.Replace('[', ' ').Replace(']', ' '));
            return strBuilder.ToString();
        }

    }
}
