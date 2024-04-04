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


        public void ComposeFilesByExtension()
        {

            foreach (var file in _fileScan.GetFiles(_directoryManipulator.GetCurrentDirecrotryPath()))
            {
                var newDirectoryPath = Path.Combine(_directoryManipulator.GetCurrentDirecrotryPath(),
                    file.Extension);
                _directoryManipulator.CreateDirectory(newDirectoryPath);
                File.Move(file.Path, Path.Combine(newDirectoryPath, file.Name));
            }
            _directoryManipulator.FillingDirecrotryList();
        }

        public void ComposeFilesByType()
        {
            foreach (var file in _fileScan.GetFiles(_directoryManipulator.GetCurrentDirecrotryPath()))
            {
                var newDirectoryPath = Path.Combine(_directoryManipulator.GetCurrentDirecrotryPath(),
                    file.Type);
                _directoryManipulator.CreateDirectory(newDirectoryPath);
                File.Move(file.Path, Path.Combine(newDirectoryPath, file.Name));
            }
            _directoryManipulator.FillingDirecrotryList();
        }

        public void ComposeFilesByLastWriteTime()
        {
            foreach (var file in _fileScan.GetAll(_directoryManipulator.GetCurrentDirecrotryPath()))
            {
                var newDirectoryPath = Path.Combine(_directoryManipulator.GetCurrentDirecrotryPath(),
                    file.LastModifiedDate.ToString("dd-MM-yyyy"));
                _directoryManipulator.CreateDirectory(newDirectoryPath);
                File.Move(file.Path, Path.Combine(newDirectoryPath, file.Name));
                _directoryManipulator.FillingDirecrotryList();
            }
        }
    }
}
