using FileSorter.Interfaces;
using FileSorter.Models;
using FileSorter.Services;
using Spectre.Console;
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
            AnsiConsole.Write(new FigletText("FileSorter").Color(Color.Red).Centered());
            AnsiConsole.MarkupLine("[bold underline yellow]Welcome to FileSorter![/]");
            AnsiConsole.MarkupLine("Press '[underline red]Esc[/]' to exit or type '[underline red]exit[/]' and press 'Enter'");

            while (true)
            {
                Console.Write(_manipulator.GetCurrentDirecrotryPath() + "> ");
                string readLine = ReadLineWithCancel();
                Console.Write("\n");
                string[] parts = readLine.Split(new[] { ' ' }, 2);
                string command = parts[0];
                string arguments = parts.Length > 1 ? parts[1] : string.Empty;


                if (command.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("\nExit...");
                    break;
                }
                if (command.Equals("sort", StringComparison.OrdinalIgnoreCase))
                {
                    HandleSortingOption(arguments);
                    continue;
                }
                if (command.Equals("goto", StringComparison.OrdinalIgnoreCase))
                {
                    _manipulator.SetDirecrotryPath(arguments);
                    continue;
                }
                if (command.Equals("ll", StringComparison.OrdinalIgnoreCase))
                {
                    Browse();
                    continue;
                }

            }
        }

        private static void HandleSortingOption(string argument)
        {
            var fileComposer = new FileComposer(_manipulator);
            switch (argument)
            {
                case "e":
                    Console.WriteLine("Sorting by file extension selected.");
                    fileComposer.ComposeByExtension();
                    break;
                case "l":
                    Console.WriteLine("Sorting by file size selected.");
                    fileComposer.ComposeByLastWriteTime();
                    break;
                default:
                    Console.WriteLine();
                    break;
            }
        }

        private static void Browse()
        {
            
            var list = _manipulator.GetDirectoryFiles().Select(l => l.ToString()).ToList();
            List<string> files = new List<string>();
            foreach (var file in list)
            {
                if (file is null)
                    continue;
                files.Add(file);
            }
            var chosenPath = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Select [green]folder[/]!")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down)[/]")
                .AddChoices(files));
        }

        private static string ReadLineWithCancel()
        {
            string result = String.Empty;
            StringBuilder buffer = new();

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
