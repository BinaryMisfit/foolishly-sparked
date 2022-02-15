using Sims.Toolkit.Api.Core.Interfaces;

namespace Sims.Toolkit.Api.Core;

/// <inheritdoc />
public class GameInstance : IGameInstance
{
    /// <summary>
    ///     Initializes an instance of <see cref="GameInstance" />.
    /// </summary>
    /// <param name="path">The installed path of the game.</param>
    /// <param name="platform">The platform the game is installed on.</param>
    public GameInstance(string path, string platform)
    {
        GamePath = path;
        InstalledPacks = new PackCollection();
        Platform = platform;
    }

    /// <inheritdoc />
    public string GamePath { get; }

    /// <inheritdoc />
    public PackCollection InstalledPacks { get; }

    /// <inheritdoc />
    public string Platform { get; }
}
