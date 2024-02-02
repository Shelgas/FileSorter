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
        private string _path;
        public FileScanner(string path)
        {
            _path = path;
        }

        public IEnumerable<FileModel> GetAll()
        {
            var fileList = new List<FileModel>();
            foreach (var filePath in Directory.GetFiles(_path))
            {
                fileList.Add(new FileModel(new FileInfo(filePath)));
            }
            return fileList;
        }
    }
}
