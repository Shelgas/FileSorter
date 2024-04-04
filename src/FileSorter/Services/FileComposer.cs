using FileSorter.Interfaces;
using FileSorter.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FileSorter.Services
{
    public class FileComposer : IFileComposer
    {

        public void ComposeFilesBy(IEnumerable<AbstractModel> files, Func<AbstractModel, string> option, string targetPath)
        {
            foreach (var file in files)
            {
                var newDirectoryPath = Path.Combine(targetPath, option(file));
                CreateDirectory(newDirectoryPath);
                File.Move(file.Path, Path.Combine(newDirectoryPath, file.Name));
            }
        }

        private void CreateDirectory(string path)
        {
            if (!Path.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
