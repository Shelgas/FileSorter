using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSorter.Services
{
    public static class DirectoryCreator
    {
        public static string Create(string path, string name)
        {
            var fullPath = Path.Combine(path, name);
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
            return fullPath;
        }
    }
}
