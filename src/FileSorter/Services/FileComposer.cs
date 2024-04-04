using FileSorter.Interfaces;
using FileSorter.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FileSorter.Services
{
    public class FileComposer : IFileComposer
    {
        private IFileScan _fileScan;
        private IDirectoryManipulator _directoryManipulator;

        public FileComposer(IDirectoryManipulator directoryManipulator, IFileScan fileScan)
        {
            _directoryManipulator = directoryManipulator;
            _fileScan = fileScan;
        }

        public void ComposeFilesBy(IEnumerable<AbstractModel> files, Func<AbstractModel, string> option, string targetPath)
        {
            foreach (var file in files)
            {
                var newDirectoryPath = Path.Combine(targetPath, option(file));
                _directoryManipulator.CreateDirectory(newDirectoryPath);
                File.Move(file.Path, Path.Combine(newDirectoryPath, file.Name));
            }
            _directoryManipulator.FillingDirecrotryList();
        }
    }
}
