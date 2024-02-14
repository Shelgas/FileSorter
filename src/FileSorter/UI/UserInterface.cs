using FileSorter.Interfaces;
using FileSorter.Models;
using FileSorter.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FileSorter.UI
{
    public static class UserInterface
    {
        private static readonly IDirectoryManipulator _manipulator = new DirectoryManipulator();
        public static void Start()
        {
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                ShowHeader("Menu");
                var selectedOptions = ShowOptions();


                if (selectedOptions.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("\nExit...");
                    break;
                }
                if (selectedOptions.Equals("sort", StringComparison.OrdinalIgnoreCase))
                {
                    //HandleSortingOption(arguments);
                    continue;
                }
                if (selectedOptions.Equals("goto", StringComparison.OrdinalIgnoreCase))
                {
                    SelectDirectory();
                    continue;
                }
                if (selectedOptions.Equals("ll", StringComparison.OrdinalIgnoreCase))
                {
                    BrowseDirectory();
                    continue;
                }

            }
        }


        private static string ShowOptions()
        {
            var commandList = new List<string>() { "Goto", "Sort" ,"Exit",  };
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Select [green]command[/]!")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down)[/]")
                .AddChoices(commandList));
        }

        private static void HandleSortingOption(string argument)
        {
            var fileComposer = new FileComposer(_manipulator);
            switch (argument)
            {
                case "e":
                    Console.WriteLine("Sorting by file extension selected.");
                    fileComposer.ComposeFilesByExtension();
                    break;
                case "l":
                    Console.WriteLine("Sorting by file size selected.");
                    fileComposer.ComposeFilesByLastWriteTime();
                    break;
                default:
                    Console.WriteLine();
                    break;
            }
        }

        private static void SelectDirectory()
        {
            while (true)
            {
                var selectedFile = BrowseDirectory();
                var path = _manipulator.GetCurrentDirecrotryPath();
                if (path.Length <= 3 && selectedFile.Equals("\u2B8C"))
                {
                    _manipulator.SetDirecrotryPath(BrowseDrive());
                    continue;
                }
                if (selectedFile.Equals("\u2B8C"))
                    _manipulator.SetDirecrotryPath(SubstringPath(path));
                if (Directory.Exists(Path.Combine(path, selectedFile)))
                    _manipulator.SetDirecrotryPath(Path.Combine(path, selectedFile));
                if (selectedFile.Equals("\u2713"))
                    break;
            }   
        }

        private static string BrowseDirectory()
        {
            ShowHeader("Browse Directory");
            var list = _manipulator.GetDirectoryFiles().Select(l => l.ToString()).ToList();
            List<string> files = new List<string>() { "\u2713", "\u2B8C" };
            files.AddRange(list);
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Select [green]folder[/]!")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down)[/]")
                .AddChoices(files));
        }

        private static string BrowseDrive()
        {
            ShowHeader("Browse Drive");
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Select [green]drive[/]!")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down)[/]")
                .AddChoices(DriveInfo.GetDrives().Select(l => l.Name).ToList()));
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

        private static string SubstringPath(string path)
        {
            for (int i = path.Length - 3; i > 0; i--)
            {
                if (path[i].ToString() == @"\")
                {
                    return path.Substring(0, i + 1);
                }
            }
            return path;
        }

        private static void ShowHeader(string ruleName)
        {
            Console.Clear();
            AnsiConsole.Write(new FigletText("FileSorter").Color(Color.Red).Centered());
            var rule = new Rule($"[red]{ruleName}[/]");
            AnsiConsole.Write(rule.Justify(Justify.Left));
            var path = new TextPath(_manipulator.GetCurrentDirecrotryPath())
                .RootColor(Color.Red)
                .SeparatorColor(Color.Green)
                .StemColor(Color.Blue)
                .LeafColor(Color.Yellow);
            Console.Write("Current path:");
            AnsiConsole.Write(path);
            AnsiConsole.Write("\n");

        }


    }
}
