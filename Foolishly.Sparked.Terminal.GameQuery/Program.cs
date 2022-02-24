using System.CommandLine;
using Foolishly.Sparked.Core;
using Microsoft.Extensions.DependencyInjection;

var cmd = new RootCommand();
cmd.SetHandler(
    () =>
    {
        var services = new ServiceCollection().ConfigureSims()
            .AddApiGame();
        var game = Game.CreateDefaultGameBuilder()
            .Build();
    });
cmd.InvokeAsync(args);
