using FileSorter.Interfaces;
using FileSorter.Models;
using FileSorter.Models.MenuOptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Spectre.Console;
using System.Text;

namespace FileSorter.UI
{
    public class UserInterface
    {
        private readonly IDirectoryManipulator _manipulator;

        public UserInterface(IDirectoryManipulator manipulator)
        {
            _manipulator = manipulator;
        }


        public void Start()
        {
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                ShowHeader("Menu");
                var selectedOptions = ShowOptions(OptionsGetter.GetOptions(typeof(StartMenuOption)));

                if (selectedOptions.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("\nExit...");
                    break;
                }
                if (selectedOptions.Equals("sort", StringComparison.OrdinalIgnoreCase))
                {
                    HandleSortingOption();
                    continue;
                }
                if (selectedOptions.Equals("goto", StringComparison.OrdinalIgnoreCase))
                {
                    SelectDirectory();
                    continue;
                }
                if (selectedOptions.Equals("show", StringComparison.OrdinalIgnoreCase))
                {
                    GetAllFiles();
                    continue;
                }
            }
        }


        private string ShowOptions(List<string> options, string title = "Select [green]command[/]!", bool useBack = false) 
        {
            List<string> optionsList = new List<string>();
            if (useBack) optionsList.Add("\u2B8C");

            optionsList.AddRange(options);
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title(title)
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down)[/]")
                .AddChoices(optionsList));
        }

        private void HandleSortingOption()
        {

            while (true)
            {
                var selectedOptions = ShowOptions(
                OptionsGetter.GetOptions(typeof(SortMenuOption)),
                "Select compose options!", true);
                if (selectedOptions == "⮌")
                    break;
                var filterOptions = GetDateFilter();

                if (filterOptions != "⮌")
                {
                    _manipulator.ComposeFile(selectedOptions, filterOptions);
                    break;
                }
                
            }
            
        }

        private string GetDateFilter()
        {
            List<string> dateFilterOptions = new List<string>() { "today", "in 7 days", "for a month", "all time" };
            return ShowOptions(dateFilterOptions, "Select [green]range[/]!", true);

        }


        private void SelectDirectory()
        {
            while (true)
            {
                var selectedFile = GetDirectories();
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


        private string GetDirectories()
        {
            ShowHeader("Browse Directory");
            List<string> files = new List<string>() { "\u2713", "\u2B8C" };
            files.AddRange(_manipulator.GetSubDirectories().Select(l => l.ToString()).ToList());
            return ShowOptions(files, "Select [green]folder[/]!");
        }

        private string GetAllFiles()
        {
            ShowHeader("Browse Directory");
            var fileList = _manipulator.GetDirectoryAllObjects().Select(l => l.ToString()).ToList();
            return ShowOptions(fileList, "Select [green]folder[/]!");
        }

        private string BrowseDrive()
        {
            ShowHeader("Browse Drive");
            var driveList = DriveInfo.GetDrives().Select(l => l.Name).ToList();
            return ShowOptions(driveList, "Select [green]drive[/]!");
        }


        private string SubstringPath(string path)
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

        private void ShowHeader(string ruleName)
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
