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
            _fileScan = new FileScanner(_directoryManipulator.GetCurrentDirecrotryPath());
        }

        public void ComposeByExtension()
        {

            foreach (var file in _fileScan.GetAll())
            {
                var newDirectoryPath = Path.Combine(_directoryManipulator.GetCurrentDirecrotryPath(),
                    file.FileExtension);
                _directoryManipulator.CreateDirectory(newDirectoryPath);
                File.Move(file.FilePath, Path.Combine(newDirectoryPath, file.FileName));
            }
        }

        public void ComposeByLastWriteTime()
        {
            foreach (var file in _fileScan.GetAll())
            {
                var newDirectoryPath = Path.Combine(_directoryManipulator.GetCurrentDirecrotryPath(), 
                    file.LastModifiedDate.ToString("dd-MM-yyyy"));
                _directoryManipulator.CreateDirectory(newDirectoryPath);
                File.Move(file.FilePath, Path.Combine(newDirectoryPath, file.FileName));

            }
        }
    }
}
