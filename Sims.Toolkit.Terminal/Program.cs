using System;
using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Sims.Toolkit.Api;
using Sims.Toolkit.Api.Core;
using Sims.Toolkit.Plugin.Manager;

namespace Sims.Toolkit.Terminal;

internal static class Program
{
    internal static int Main(string[] args)
    {
        var services = new ServiceCollection().AddSimsToolkitApi()
            .BuildServiceProvider();
        var plugins = new PluginCollection().AddToolkitPlugins()
            .BuildPluginProvider();
        var commandGame = new Command("game", "Prints information about the game.");
        commandGame.SetHandler(
            () =>
            {
                var instance = services.GetService<IGameInstance>();
                var game = instance?.AddGamePacks()
                    .BuildGameProvider();
                var progress = new Progress<ProgressReport>();
                progress.ProgressChanged += (_, e) => { Console.WriteLine(e.Message); };
                Console.WriteLine(game?.PrintGameInfo());
            });
        var commandLine = new RootCommand {Description = "Command line interface for the Sims Toolkit."};
        commandLine.AddCommand(commandGame);
        return commandLine.InvokeAsync(args)
            .Result;
    }
}
