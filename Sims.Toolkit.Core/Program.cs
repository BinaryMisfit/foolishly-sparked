using System;
using System.CommandLine;
using System.IO;
using Sims.Toolkit.Api.Core;
using Sims.Toolkit.Api.Helpers;

namespace Sims.Toolkit.Core;

internal static class Program
{
    private static int Main(string[] args)
    {
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
            try
            {
                var game = Game.LoadPlugin();
                Console.WriteLine($"Running on {game.Platform} {(game.Is64 ? "64-Bit" : "")}.");
                await game.LocateGameAsync();
                Console.WriteLine($"Located game at {game.InstalledPath}.");
                Console.WriteLine($"Reading package {packageFile.Name} in {packageFile.DirectoryName}.");
                var progress = new Progress<ProgressReport>();
                progress.ProgressChanged += (_, e) => { Console.WriteLine(e.Message); };
                var pack = new Package(packageFile);
                await pack.LoadPackageAsync();
                await pack.LoadPackageContentAsync();
                Console.WriteLine(
                    $"Loaded package {pack} successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.GetType().Name}: {e.Message}");
            }
        }, packageArgument);
        return commandLine.InvokeAsync(args).Result;
    }
}
