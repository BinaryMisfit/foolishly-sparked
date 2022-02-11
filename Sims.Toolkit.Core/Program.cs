using System;
using System.CommandLine;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Sims.Toolkit.Api;
using Sims.Toolkit.Api.Helpers;
using Sims.Toolkit.Api.Interfaces;

namespace Sims.Toolkit.Core;

internal static class Program
{
    internal static int Main(string[] args)
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        var packageArgument = new Argument<FileInfo>(
            "PackageFile",
            "Full path to the .package file."
        ).ExistingOnly();
        var commandLine = new RootCommand
        {
            packageArgument
        };
        commandLine.Description = "Configures and updates Sims 4 .package files for compatibility.";
        commandLine.SetHandler(async (FileInfo packageFile) =>
        {
            var provider = services.BuildServiceProvider();
            try
            {
                var game = (IGame) provider.GetService(typeof(IGame));
                var platform = game.LoadPlugin();
                Console.WriteLine($"Running on {platform.Platform} {(platform.Is64 ? "64-Bit" : "")}.");
                await platform.LocateGameAsync();
                Console.WriteLine($"Located game at {platform.InstalledPath}.");
                Console.WriteLine($"Reading {packageFile.Name} in {packageFile.DirectoryName}.");
                var progress = new Progress<ProgressReport>();
                progress.ProgressChanged += (_, e) => { Console.WriteLine(e.Message); };
                var pack = (IPackage) provider.GetService(typeof(IPackage));
                pack = pack.LoadFromFile(packageFile.FullName);
                await pack.LoadPackageAsync();
                await pack.LoadPackageContentAsync();
                Console.WriteLine(
                    $"Loaded {pack} successfully.");
            }
            catch (EndOfStreamException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (FileLoadException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (TaskCanceledException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (VersionNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }, packageArgument);
        return commandLine.InvokeAsync(args).Result;
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        Configure.ConfigureServices(services);
    }
}
