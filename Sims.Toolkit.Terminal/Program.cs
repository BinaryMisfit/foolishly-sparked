using System;
using System.CommandLine;
using Sims.Toolkit.Api;
using Sims.Toolkit.Plugin.Manager;
using Sims.Toolkit.Plugin.Properties;

namespace Sims.Toolkit.Terminal;

internal static class Program
{
    internal static int Main(string[] args)
    {
        var plugins = new PluginCollection().AddToolkitPlugins()
            .BuildPluginProvider();
        var commandGame = new Command("game", "Prints information about the game.");
        commandGame.SetHandler(
            () =>
            {
                var progress = new Progress<ProgressReport>();
                foreach (var plugin in plugins.GetPluginList())
                {
                    Console.WriteLine(plugin);
                }

                progress.ProgressChanged += (_, e) => { Console.WriteLine(e.Message); };
                var locator = plugins.GetPlugin("GameLoader", Environment.OSVersion.Platform);
                if (locator == null)
                {
                    throw new DllNotFoundException(Exceptions.PluginMissingPlatform);
                }

                locator.Execute();
            });
        var commandLine = new RootCommand {Description = "Command line interface for the Sims Toolkit."};
        commandLine.AddCommand(commandGame);
        return commandLine.InvokeAsync(args)
            .Result;
    }
}
