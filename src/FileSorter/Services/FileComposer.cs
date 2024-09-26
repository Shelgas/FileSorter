using FileSorter.Interfaces;
using FileSorter.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FileSorter.Services
{
    public class FileComposer : IFileComposer
    {
        private readonly bool directoriesAllowed;   

        public FileComposer(IConfiguration configuration)
        {
            directoriesAllowed = bool.Parse(configuration
                .GetSection("ComposeParametrs")
                .GetSection("AllowedDirectory").Value ?? "false");
        }

        public void ComposeFilesBy(
            IEnumerable<AbstractModel> files,
            Func<AbstractModel, string> option, string targetPath,
            Func<DateTime, bool>? filter = null)
        {
            foreach (var file in files)
            {
                if (filter != null && !filter(file.LastModifiedDate))
                    continue;
                var newDirectoryPath = Path.Combine(targetPath, option(file));
                if (file.Type == "Directory" && !directoriesAllowed)
                    continue;
                        
                CreateDirectory(newDirectoryPath);
                if (file.Type == "Directory")
                {
                    if (file.Path == newDirectoryPath)
                        continue;
                    Directory.Move(file.Path, Path.Combine(newDirectoryPath, file.Name));
                }
                else
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
