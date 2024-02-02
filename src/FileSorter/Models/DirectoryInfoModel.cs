using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSorter.Models
{
    public class DirectoryInfoModel
    {
        public string DirectoryPath { get; set;  }
        public List<FileModel> Files { get; set; }

        public DirectoryInfoModel()
        {
            Files = new List<FileModel>();
            DirectoryPath = Path.GetFullPath($@"C:\Users\{Environment.UserName}");
        }

      
    }
}
