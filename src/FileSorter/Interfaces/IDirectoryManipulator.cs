using FileSorter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSorter.Interfaces
{
    public interface IDirectoryManipulator
    {
        public void CreateDirectory(string directoryName);
        public void SetDirecrotryPath(string path);
        public string GetCurrentDirecrotryPath();
        public IEnumerable<AbstractModel> GetDirectoryAllObjects();
        public IEnumerable<AbstractModel> GetSubDirectories();
        public void FillingDirecrotryList();
        public void ComposeFile(string selectedOptions);
    }
}
