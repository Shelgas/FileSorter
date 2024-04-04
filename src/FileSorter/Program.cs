using FileSorter.Interfaces;
using FileSorter.Models;
using FileSorter.Services;
using FileSorter.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FileSorter;
class Program
{
    static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder();
        BuildConfig(builder);
        IConfiguration config = builder.Build();
        IHost host = CreateHost(config);
        var ui = ActivatorUtilities.CreateInstance<UserInterface>(host.Services);
        ui.Start();
    }

    static void BuildConfig(IConfigurationBuilder builder)
    {
        builder.SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    }

    private static IHost CreateHost(IConfiguration config) =>
        Host.CreateDefaultBuilder()
        .ConfigureServices((context, services) =>
        {
            services.AddSingleton<IDirectoryManipulator, DirectoryManipulator>();
            services.AddSingleton<IFileScan, FileScanner>();
            services.AddSingleton<IFileComposer, FileComposer>();
            services.AddSingleton(config.GetSection("FileTypes").Get<Dictionary<string, List<string>>>());
        })
        .Build();
}
