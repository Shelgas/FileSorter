using FileSorter.Interfaces;
using FileSorter.Models;
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
        private static readonly IDirectoryManipulator _manipulator = new DirectoryManipulator();
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
                Console.Write("\n" +_manipulator.GetCurrentDirecrotryPath() + "> ");
                string readLine = ReadLineWithCancel();
                string[] parts = readLine.Split(new[] { ' ' }, 2);
                string command = parts[0];
                string arguments = parts.Length > 1 ? parts[1] : string.Empty;


                if (command.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("\nExit...");
                    break;
                }
                if (command.Equals("sorte", StringComparison.OrdinalIgnoreCase))
                {
                    HandleSortingOption(readLine);
                    continue;
                }
                if (command.Equals("goto", StringComparison.OrdinalIgnoreCase))
                {
                    _manipulator.SetDirecrotryPath(arguments);
                    continue;
                }

            }
        }

        private static void HandleSortingOption(string command)
        {
            var fileComposer = new FileComposer(_manipulator);
            switch (command)
            {
                case "sort_e":
                    Console.WriteLine("Sorting by file extension selected.");
                    fileComposer.ComposeByExtension();
                    break;
                case "sort_l":
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
