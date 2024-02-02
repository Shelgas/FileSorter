using FileSorter.Interfaces;
using FileSorter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSorter.Services
{
    public class DirectoryManipulator : IDirectoryManipulator
    {
        private readonly DirectoryInfoModel _directoryInfoModel;
        private readonly IFileScan _fileScan;
        public DirectoryManipulator()
        {
            _directoryInfoModel = new DirectoryInfoModel();
            _fileScan = new FileScanner(_directoryInfoModel.DirectoryPath);
            FillingDirecrotryList();
        }

        public void CreateDirectory (string path)
        {
            if (!Path.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public string GetCurrentDirecrotryPath()
        {
            return _directoryInfoModel.DirectoryPath;
        }

        public void SetDirecrotryPath(string path)
        {
            if (Path.Exists(path))
            {
                _directoryInfoModel.DirectoryPath = path;
            }

        }

        private void FillingDirecrotryList()
        {
            _directoryInfoModel.Files.Clear();
            foreach (var file in _fileScan.GetAll())
                _directoryInfoModel.Files.Add(file);
        }
    }
}
