using FileSorter.Interfaces;
using FileSorter.Models;

namespace FileSorter.Services
{
    public class FileScanner : IFileScan
    {
        private readonly Dictionary<string, List<string>> _fileTypeMappings;

        public FileScanner(Dictionary<string, List<string>> fileTypeMappings)
        {
            _fileTypeMappings = fileTypeMappings;
        }
        public IEnumerable<AbstractModel> GetAll(string path)
        {
 
            var fileList = new List<AbstractModel>();
            fileList.AddRange(GetDirectories(path));
            fileList.AddRange(GetFiles(path));
            return fileList;
        }

        public IEnumerable<FileModel> GetFiles(string path)
        {
            var options = new EnumerationOptions
            {
                IgnoreInaccessible = true,
                RecurseSubdirectories = false,

            };
            var fileList = new List<FileModel>();
            foreach (var filePath in Directory.GetFiles(path, "*", options))
            {
                var fileInfo = new FileInfo(filePath);
                var type = GetFileType(fileInfo.Extension);
                fileList.Add(new FileModel(fileInfo, type));
            }
            return fileList;
        }

        public IEnumerable<DirectoryModel> GetDirectories(string path)
        {
            var options = new EnumerationOptions
            {
                IgnoreInaccessible = true,
                RecurseSubdirectories = false,

            };

            var directoryList = new List<DirectoryModel>();
            foreach (var filePath in Directory.GetDirectories(path, "*", options))
            {
                var directoryInfo = new DirectoryInfo(filePath);
                if (directoryInfo.Name[0] != '.')
                {
                    directoryList.Add(new DirectoryModel(new DirectoryInfo(filePath)));
                }    
            }
            return directoryList;
        }

        private string GetFileType(string extension)
        {
            foreach (var item in _fileTypeMappings)
            {
                foreach (var t in item.Value)
                {
                    if (t == extension) return item.Key;
                }
            }
            return extension.Substring(1);
        }
    }
}
