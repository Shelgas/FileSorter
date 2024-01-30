using FileSorter.Services;
using System.Text;

namespace FileSorter;
class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        var a = new FileScanner(@"C:\Users\akozl\Downloads");

        Console.WriteLine("ПРивет");
    }
}
