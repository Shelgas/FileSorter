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
        private List<FileModel> fileList;
        private string _path;
        public FileScanner(string path)
        {
            fileList = new List<FileModel>();
            _path = path;
        }

        public IEnumerable<FileModel> GetAll()
        {
            foreach (var filePath in Directory.GetFiles(_path))
            {
                fileList.Add(new FileModel(new FileInfo(filePath)));
            }
            return fileList;
        }
    }
}
