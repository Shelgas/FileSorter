using FileSorter.Models;

namespace FileSorter.Interfaces
{
    public interface IFileComposer
    {
        void ComposeFilesBy(IEnumerable<AbstractModel> files, Func<AbstractModel, string> option, string targetPath, Func<DateTime, bool>? filter = null);
    }
}