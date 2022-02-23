using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Sims.Api.Core;
using Sims.Core;

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
