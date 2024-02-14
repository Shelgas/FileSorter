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
        private readonly DirectoryModel _directoryInfoModel;
        private readonly IFileScan _fileScan;
        public DirectoryManipulator()
        {
            _directoryInfoModel = new DirectoryModel(new DirectoryInfo($@"C:\Users\akozl\Downloads\Telegram Desktop\"));
                _fileScan = new FileScanner();
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
            return _directoryInfoModel.Path;
        }

        public void SetDirecrotryPath(string path)
        {
            if (Path.Exists(path))
            {
                _directoryInfoModel.Path = path;
                FillingDirecrotryList();
            }

        }

        public IEnumerable<AbstractModel> GetDirectoryFiles()
        {
            return _directoryInfoModel.Files;
        }

        private void FillingDirecrotryList()
        {
            _directoryInfoModel.Files.Clear();
            foreach (var file in _fileScan.GetAll(_directoryInfoModel.Path))
                _directoryInfoModel.Files.Add(file);
        }
    }
}
