using FileSorter.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSorter.Services
{
    public class FileCompose
    {
        private IFileScan _fileScan;
        public string DirectoryPath { get; private set; }
        public FileCompose(string path)
        {
            DirectoryPath = path;
            _fileScan = new FileScanner(DirectoryPath);
        }

        public void ComposeByExtension()
        {
            foreach (var file in _fileScan.GetAll())
            {
                var directoryPath = DirectoryCreator.Create(DirectoryPath, file.FileExtension);
                File.Move(file.FilePath, Path.Combine(directoryPath, file.FileName));
            }
        }
    }
}
