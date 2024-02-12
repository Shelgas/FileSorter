using FileSorter.Interfaces;

namespace FileSorter.Services
{
    public class FileComposer
    {
        private IFileScan _fileScan;
        private IDirectoryManipulator _directoryManipulator;

        public FileComposer(IDirectoryManipulator directoryManipulator)
        {
            
            _directoryManipulator = directoryManipulator;
            _fileScan = new FileScanner();
        }

        public void ComposeByExtension()
        {

            foreach (var file in _fileScan.GetAll(_directoryManipulator.GetCurrentDirecrotryPath()))
            {
                var newDirectoryPath = Path.Combine(_directoryManipulator.GetCurrentDirecrotryPath(),
                    file.Extension);
                _directoryManipulator.CreateDirectory(newDirectoryPath);
                File.Move(file.Path, Path.Combine(newDirectoryPath, file.Name));
            }
        }

        public void ComposeByLastWriteTime()
        {
            foreach (var file in _fileScan.GetAll(_directoryManipulator.GetCurrentDirecrotryPath()))
            {
                var newDirectoryPath = Path.Combine(_directoryManipulator.GetCurrentDirecrotryPath(), 
                    file.LastModifiedDate.ToString("dd-MM-yyyy"));
                _directoryManipulator.CreateDirectory(newDirectoryPath);
                File.Move(file.Path, Path.Combine(newDirectoryPath, file.Name));

            }
        }
    }
}
