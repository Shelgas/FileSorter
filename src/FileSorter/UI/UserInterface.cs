using FileSorter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSorter.UI
{
    public static class UserInterface
    {
        public static void Start()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Welcome to FileSorter!");
            Console.WriteLine("Please select a sorting option:");
            Console.WriteLine("SortE. Sort by file extension");
            Console.WriteLine("SortL. Sort by last modified time");
            Console.WriteLine("Press 'Esc' to exit or type 'exit' and press 'Enter'");

            while (true)
            {
                string readLine = ReadLineWithCancel();
                if (readLine == String.Empty)
                {
                    Console.WriteLine(@"\");
                    continue;
                }
                else if (readLine.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("\nExit...");
                    break;
                }
                else
                {
                    HandleSortingOption(readLine);
                    continue;
                }
                Console.WriteLine();
            }
        }

        private static void HandleSortingOption(string command)
        {
            var fileComposer = new FileComposer(@"C:\Users\akozl\Downloads\Telegram Desktop\.pdf");
            switch (command)
            {
                case "SortE":
                    Console.WriteLine("Sorting by file type selected.");
                    fileComposer.ComposeByExtension();
                    break;
                case "SortL":
                    Console.WriteLine("Sorting by file size selected.");
                    fileComposer.ComposeByLastWriteTime();
                    break;
                default:
                    Console.WriteLine();
                    break;
            }
        }

        private static string ReadLineWithCancel()
        {
            string result = String.Empty;
            StringBuilder buffer = new StringBuilder();

            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter && info.Key != ConsoleKey.Escape)
            {
                if (info.Key == ConsoleKey.Backspace && buffer.Length != 0)
                {
                    Console.Write("\b \b");
                    info = Console.ReadKey(true);
                    buffer.Remove(buffer.Length - 1, 1);
                    continue;
                }
                Console.Write(info.KeyChar);
                buffer.Append(info.KeyChar);
                info = Console.ReadKey(true);
            }

            if (info.Key == ConsoleKey.Escape)
                return "exit";

            if (info.Key == ConsoleKey.Enter)
            {
                result = buffer.ToString();
            }

            return result;
        }

    }
}
