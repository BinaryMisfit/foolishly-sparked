namespace Sims.Toolkit.Api;

/// <summary>
///     Extensions methods to build a <see cref="IGameProvider" /> for an installed game instance.
/// </summary>
public static class GameBuilderExtensions
{
    /// <summary>
    ///     Returns a game instance based on the local game installation.
    /// </summary>
    /// <returns>The <see cref="IGameInstance" />.</returns>
    public static IGameProvider BuildGameProvider(this IGameInstance gameInstance)
    {
        return new GameProvider(gameInstance);
    }

    /// <summary>
    ///     Add all installed game packs to the existing <see cref="IGameInstance" />.
    /// </summary>
    /// <param name="gameInstance">The <see cref="IGameInstance" />.</param>
    /// <returns>The <see cref="IGameInstance" />.</returns>
    public static IGameInstance AddGamePacks(this IGameInstance gameInstance)
    {
        gameInstance = ((IGameLocator) gameInstance).LocateGame();
        gameInstance = ((IGameLocator) gameInstance).LocateGamePacks();
        return gameInstance;
    }
}
