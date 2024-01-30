using FileSorter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSorter.Services
{
    public class FileScanner
    {
        public List<FileModel> FileList { get; set; }
        public FileScanner(string path)
        {
            FileList = new List<FileModel>();
            var filePaths = Directory.GetFiles(path);
            foreach (var filePath in filePaths)
            {
                FileList.Add(new FileModel(new FileInfo(filePath)));
            }
            var uniqueExtensions = FileList
               .Select(file => file.FileExtension.ToLower()) // Преобразуем все расширения в нижний регистр
               .Distinct().ToList();
            foreach (var item in uniqueExtensions)
            {
                Console.WriteLine(item);
            }
        }
    }
}
