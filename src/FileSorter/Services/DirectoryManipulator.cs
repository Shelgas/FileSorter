using FileSorter.Interfaces;
using FileSorter.Models;
using Microsoft.Extensions.Options;

namespace FileSorter.Services
{
    public class DirectoryManipulator : IDirectoryManipulator
    {
        private readonly DirectoryModel _directoryInfoModel;
        private readonly IFileScan _fileScan;
        private readonly IFileComposer _fileComposer;

        public DirectoryManipulator(IFileScan fileScan, IFileComposer fileComposer, DirectoryModel directoryModel)
        {
            _directoryInfoModel = directoryModel;
            _fileScan = fileScan;
            _fileComposer = fileComposer;
            FillingDirecrotryList();
        }

        public void ComposeFile(string selectedOptions, string filterOption)
        {
            Func<AbstractModel, string> func = (x) => (x.Type);
            Func<DateTime, bool>? filter = GetFilter(filterOption);
            switch (selectedOptions)
            {
                case "Extension":
                    func = (x) => (x.Extension);
                    break;
                case "Type":
                    func = (x) => (x.Type);
                    break;
                case "Date":
                    func = (x) => (x.GetDate());
                    break;
            }
            _fileComposer.ComposeFilesBy(GetDirectoryAllObjects(), func, GetCurrentDirecrotryPath(), filter);
            FillingDirecrotryList();
        }

        private Func<DateTime, bool>? GetFilter(string option)
        {
            var dayCount = GetDayCount(option);
            if (dayCount != null)
                return x => x > DateTime.Now.AddDays(-(int)dayCount).Date;
            return null;
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

        public IEnumerable<AbstractModel> GetDirectoryAllObjects()
        {
            return _directoryInfoModel.Files;
        }

        public IEnumerable<AbstractModel> GetDirectoryFiles()
        {
            return _directoryInfoModel.Files.Where(x => x.Type != "Directory");
        }

        public IEnumerable<AbstractModel> GetSubDirectories()
        {
            return _directoryInfoModel.Files.Where(x => x.Type == "Directory");
        }

        public void FillingDirecrotryList()
        {
            _directoryInfoModel.Files.Clear();
            foreach (var file in _fileScan.GetAll(_directoryInfoModel.Path))
                _directoryInfoModel.Files.Add(file);
        }

        private int? GetDayCount(string options)
        {
            int? result = options switch
            {
                "today" => 0,
                "in 7 days" => 7,
                "for a month" => 365,
                "all time" => null,
                _ => 0
            };
            return result;
        }
    }
}
