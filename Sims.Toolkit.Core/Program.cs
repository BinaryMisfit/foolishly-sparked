using System;
using System.CommandLine;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Sims.Toolkit.Api;
using Sims.Toolkit.Api.Assets.Properties;
using Sims.Toolkit.Api.Core.Interfaces;
using Sims.Toolkit.Api.Helpers;
using Sims.Toolkit.Api.Helpers.Interfaces;

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
                var progress = new Progress<ProgressReport>();
                progress.ProgressChanged += (_, e) => { Console.WriteLine(e.Message); };
                var loader = (IGameLoader) provider.GetService(typeof(IGameLoader));
                var plugin = loader.LoadPlatformPlugin();
                Console.WriteLine(ConsoleOutput.PrintPlatform, plugin.Platform, plugin.Is64 ? "64-Bit" : "");
                var platform = await plugin.LocateGameAsync();
                Console.WriteLine(ConsoleOutput.PrintGameFound, platform.InstalledPath);
                var game = await loader.LoadGameAsync(platform.InstalledPath, platform.Platform, progress);
                game.Packs.Summary().ToList()
                    .ForEach(item => Console.WriteLine(ConsoleOutput.PrintKeyValue, item.Key, item.Value));
                Console.WriteLine(ConsoleOutput.PrintReadingFrom, packageFile.Name, packageFile.DirectoryName);
                game.Packs.OrderBy(pack => pack.PackType).ThenBy(pack => pack.PackTypeId).ToList()
                    .ForEach(pack => { Console.WriteLine(ConsoleOutput.PrintPackName, pack.PackName); });
                var pack = (IPackage) provider.GetService(typeof(IPackage));
                pack = pack.LoadFromFile(packageFile.FullName);
                await pack.LoadPackageAsync();
                await pack.LoadPackageContentAsync();
                Console.WriteLine(ConsoleOutput.PrintLoadedPackage, pack);
                pack.Contents.Summary().ToList()
                    .ForEach(item => Console.WriteLine(ConsoleOutput.PrintKeyValue, item.Key, item.Value));
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
