namespace Sims.Api.Core;

/// <summary>
///     Extensions methods to build a <see cref="IGameProvider" /> for an installed game instance.
/// </summary>
public static class GameBuilderExtensions
{
    public static Game Build(this IGameBuilder builder)
    {
        var game = new Game();
        game.AddGameBuilder(builder);
        return game;
    }

    public static IGameProvider BuildGameProvider(this IGameProvider provider)
    {
        return provider;
    }

    public static IGameProvider AddBase(this IGameProvider provider)
    {
        return provider;
    }

    public static IGameProvider AddPacks(this IGameProvider provider)
    {
        return provider;
    }
}
