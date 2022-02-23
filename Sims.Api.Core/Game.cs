namespace Sims.Api.Core;

public class Game
{
    private IServiceProvider? _provider;

    internal void AddGameBuilder(IGameBuilder builder)
    {
        _provider = builder.CreateProvider();
    }

    public static IGameBuilder CreateDefaultGameBuilder()
    {
        return new GameBuilder();
    }
}
