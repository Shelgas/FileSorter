using FileSorter.Services;
using System.Text;

namespace FileSorter;
class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        var fileCompose = new FileCompose(@"C:\Users\akozl\Downloads\Telegram Desktop");
        fileCompose.ComposeByExtension();
    }
}
