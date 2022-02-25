namespace Foolishly.Sparked.Core;

/// <summary>
///     Extensions methods to build a <see cref="IGameBuilder" /> for an installed game instance.
/// </summary>
public static class GameBuilderExtensions
{
    public static Game Build(this IGameBuilder builder)
    {
        var game = new Game();
        (game as IGameInternals).AddGameBuilder(builder.Configure());
        return game;
    }
}
