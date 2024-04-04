using FileSorter.Interfaces;
using FileSorter.Models;

namespace FileSorter.Services
{
    public class DirectoryManipulator : IDirectoryManipulator
    {
        private readonly DirectoryModel _directoryInfoModel;
        private readonly IFileScan _fileScan;
        private readonly IFileComposer _fileComposer;

        public DirectoryManipulator(IFileScan fileScan, IFileComposer fileComposer)
        {
            _directoryInfoModel = new DirectoryModel(new DirectoryInfo($@"C:\Users\akozl\Downloads\Telegram Desktop\"));
            _fileScan = fileScan;
            _fileComposer = fileComposer;
            FillingDirecrotryList();
        }

        public void ComposeFile(string selectedOptions)
        {
            Func<AbstractModel, string> func = (x) => (x.Type);
            switch (selectedOptions)
            {
                case "Extension":
                    func = (x) => (x.Extension);
                    break;
                case "Type":
                    func = (x) => (x.Type);
                    break;
            }
            _fileComposer.ComposeFilesBy(GetDirectoryAllObjects(), func, GetCurrentDirecrotryPath());
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
    }
}
