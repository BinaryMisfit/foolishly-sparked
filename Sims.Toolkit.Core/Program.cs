using System;
using System.CommandLine;
using System.IO;
using Sims.Toolkit.Api.Core;
using Sims.Toolkit.Api.Helpers;

namespace Sims.Toolkit.Core
{
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
                Console.WriteLine($"Reading package file: {packageFile.Name}");
                try
                {
                    var progress = new Progress<ProgressReport>();
                    progress.ProgressChanged += (_, e) => { Console.WriteLine(e.Message); };
                    var pack = new Package(packageFile);
                    await pack.LoadPackageAsync(progress);
                    Console.WriteLine($"{packageFile.Name} loaded successfully.");
                }
                catch (NotSupportedException)
                {
                    Console.WriteLine($"{packageFile.Name} does not contain any CAS parts.");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.GetType().Name}: {e.Message}");
                }
            }, packageArgument);
            return commandLine.InvokeAsync(args).Result;
        }
    }
}
