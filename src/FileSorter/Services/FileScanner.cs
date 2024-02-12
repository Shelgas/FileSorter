using FileSorter.Interfaces;
using FileSorter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSorter.Services
{
    public class FileScanner : IFileScan
    {

        public IEnumerable<FileModel> GetAll(string path)
        {
            var b = new DirectoryInfo(path);
            var fileList = new List<FileModel>();
            var a = Directory.GetDirectories(path);
            foreach (var filePath in Directory.GetFiles(path))
            {
                fileList.Add(new FileModel(new FileInfo(filePath)));
            }
            return fileList;
        }
    }
}
