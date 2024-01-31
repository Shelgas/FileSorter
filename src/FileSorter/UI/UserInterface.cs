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
            Console.WriteLine("1. Sort by file extension");
            Console.WriteLine("2. Sort by last modified time");
            Console.WriteLine("Press 'Esc' to exit or type 'exit' and press 'Enter'");

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("Exiting...");
                    break;
                }
                else if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.D3)
                {
                    HandleSortingOption(key.Key);
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    string input = Console.ReadLine().ToLower();
                    if (input == "exit")
                    {
                        Console.WriteLine("Exiting...");
                        break;
                    }
                }
            }
        }

        private static void HandleSortingOption(ConsoleKey key)
        {
            var fileCompose = new FileCompose(@"C:\Users\akozl\Downloads\Telegram Desktop\.docx");
            switch (key)
            {
                case ConsoleKey.D1:
                    Console.WriteLine("Sorting by file type selected.");
                    fileCompose.ComposeByExtension();
                    break;
                case ConsoleKey.D2:
                    Console.WriteLine("Sorting by file size selected.");
                    fileCompose.ComposeByLastWriteTime();
                    break;
            }
        }

    }
}
