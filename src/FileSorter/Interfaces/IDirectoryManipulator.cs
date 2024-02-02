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
    }
}
