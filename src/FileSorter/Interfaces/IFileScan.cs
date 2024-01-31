using FileSorter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSorter.Interfaces
{
    internal interface IFileScan
    {
        IEnumerable<FileModel> GetAll();
    }
}
