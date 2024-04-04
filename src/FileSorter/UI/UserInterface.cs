using FileSorter.Interfaces;
using FileSorter.Models;
using FileSorter.Models.MenuOptions;
using Microsoft.Extensions.Configuration;
using Spectre.Console;
using System.Text;

namespace FileSorter.UI
{
    public class UserInterface
    {
        private readonly IDirectoryManipulator _manipulator;
        private readonly IFileComposer fileComposer;
        private readonly IConfiguration _config;

        public UserInterface(IDirectoryManipulator manipulator, IFileComposer fileComposer)
        {
            _manipulator = manipulator;
            this.fileComposer = fileComposer;
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


        private string ShowOptions(List<string> options) 
        {
            var commandList = options;
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Select [green]command[/]!")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down)[/]")
                .AddChoices(commandList));
        }

        private void HandleSortingOption()
        {
            var selectedOptions = ShowOptions(OptionsGetter.GetOptions(typeof(SortMenuOption)));
            switch (selectedOptions)
            {
                case "Extension":
                    fileComposer.ComposeFilesBy(_manipulator.GetDirectoryAllObjects(), (x) => (x.Extension), _manipulator.GetCurrentDirecrotryPath());
                    break;
                case "cansel":
                    break;
                case "Type":
                    fileComposer.ComposeFilesBy(_manipulator.GetDirectoryAllObjects(), (x) => (x.Type), _manipulator.GetCurrentDirecrotryPath());
                    break;
                default:
                    Console.WriteLine();
                    break;
            }
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

        private string BrowsePrompt(List<string> fileList)
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Select [green]folder[/]!")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down)[/]")
                .AddChoices(fileList));
        }
        private string GetDirectories()
        {
            ShowHeader("Browse Directory");
            List<string> files = new List<string>() { "\u2713", "\u2B8C" };
            files.AddRange(_manipulator.GetSubDirectories().Select(l => l.ToString()).ToList());
            return BrowsePrompt(files);
        }

        private string GetAllFiles()
        {
            ShowHeader("Browse Directory");
            var fileList = _manipulator.GetDirectoryAllObjects().Select(l => l.ToString()).ToList();
            return BrowsePrompt(fileList);
        }

        private string BrowseDrive()
        {
            ShowHeader("Browse Drive");
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Select [green]drive[/]!")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down)[/]")
                .AddChoices(DriveInfo.GetDrives().Select(l => l.Name).ToList()));
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
