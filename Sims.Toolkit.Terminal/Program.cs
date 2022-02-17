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
using Sims.Toolkit.Terminal.Properties;

namespace Sims.Toolkit.Terminal;

internal static class Program
{
    internal static int Main(string[] args)
    {
        var services = new ServiceCollection().AddSimsToolkitApi()
            .BuildServiceProvider();
        var commandGame = new Command("game", "Prints information about the game.");
        commandGame.SetHandler(
            async () =>
            {
                try
                {
                    var progress = new Progress<ProgressReport>();
                    progress.ProgressChanged += (_, e) => { Console.WriteLine(e.Message); };
                    var loader = (IGameLoader) services.GetService(typeof(IGameLoader));
                    var game = await loader.LoadGameAsync(progress);
                    Console.WriteLine(ConsoleOutput.PrintGameFound, game.GamePath);
                    game.InstalledPacks.Summary()
                        .ToList()
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
            });
        var commandLine = new RootCommand {Description = "Command line interface for the Sims Toolkit."};
        commandLine.AddCommand(commandGame);
        return commandLine.InvokeAsync(args)
            .Result;
    }
}
