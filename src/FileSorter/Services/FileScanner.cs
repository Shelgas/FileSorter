using FileSorter.Interfaces;
using FileSorter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSorter.Services
{
    public class FileScanner : IFileScan
    {

        public IEnumerable<AbstractModel> GetAll(string path)
        {
 
            var fileList = new List<AbstractModel>();
            fileList.AddRange(GetDirectories(path));
            fileList.AddRange(GetFiles(path));
            return fileList;
        }

        public IEnumerable<FileModel> GetFiles(string path)
        {
            var options = new EnumerationOptions
            {
                IgnoreInaccessible = true,
                RecurseSubdirectories = false,

            };
            var fileList = new List<FileModel>();
            foreach (var filePath in Directory.GetFiles(path, "*", options))
            {
                fileList.Add(new FileModel(new FileInfo(filePath)));
            }
            return fileList;
        }

        public IEnumerable<DirectoryModel> GetDirectories(string path)
        {
            var options = new EnumerationOptions
            {
                IgnoreInaccessible = true,
                RecurseSubdirectories = false,

            };
            var directoryList = new List<DirectoryModel>();
            foreach (var filePath in Directory.GetDirectories(path, "*", options))
            {
                var directoryInfo = new DirectoryInfo(filePath);
                if (directoryInfo.Name[0] != '.')
                {
                    directoryList.Add(new DirectoryModel(new DirectoryInfo(filePath)));
                }    
            }
            return directoryList;
        }
    }
}
