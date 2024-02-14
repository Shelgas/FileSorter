using FileSorter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSorter.Interfaces
{
    public interface IFileScan
    {
        IEnumerable<AbstractModel> GetAll(string path);
        IEnumerable<FileModel> GetFiles(string path);
        IEnumerable<DirectoryModel> GetDirectories(string path);

    }
}
