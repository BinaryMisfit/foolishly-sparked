using System;
using System.CommandLine;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Sims.Toolkit.Api;
using Sims.Toolkit.Api.Helpers;
using Sims.Toolkit.Api.Helpers.Interfaces;
using Sims.Toolkit.Console.Properties;

namespace Sims.Toolkit.Console;

internal static class Program
{
    internal static int Main(string[] args)
    {
        var services = new ServiceCollection().AddSimsToolkitApi()
            .BuildServiceProvider();
        var commandLine = new RootCommand {Description = "Prints information regarding the current game installation."};
        commandLine.SetHandler(
            async () =>
            {
                try
                {
                    var progress = new Progress<ProgressReport>();
                    progress.ProgressChanged += (_, e) => { System.Console.WriteLine(e.Message); };
                    var loader = (IGameLoader) services.GetService(typeof(IGameLoader));
                    var game = await loader.LoadGameAsync(progress);
                    System.Console.WriteLine(ConsoleOutput.PrintGameFound, game.GamePath);
                    game.InstalledPacks.Summary()
                        .ToList()
                        .ForEach(item => System.Console.WriteLine(ConsoleOutput.PrintKeyValue, item.Key, item.Value));
                    game.InstalledPacks.OrderBy(pack => pack.PackType)
                        .ThenBy(pack => pack.PackTypeId)
                        .ToList()
                        .ForEach(pack => { System.Console.WriteLine(ConsoleOutput.PrintPackName, pack.PackName); });
                }
                catch (EndOfStreamException e)
                {
                    System.Console.WriteLine(e.Message);
                }
                catch (FileLoadException e)
                {
                    System.Console.WriteLine(e.Message);
                }
                catch (FileNotFoundException e)
                {
                    System.Console.WriteLine(e.Message);
                }
                catch (InvalidCastException e)
                {
                    System.Console.WriteLine(e.Message);
                }
                catch (TaskCanceledException e)
                {
                    System.Console.WriteLine(e.Message);
                }
                catch (VersionNotFoundException e)
                {
                    System.Console.WriteLine(e.Message);
                }
            });
        return commandLine.InvokeAsync(args)
            .Result;
    }
}
