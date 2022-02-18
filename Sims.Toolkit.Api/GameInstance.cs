using System;

namespace Sims.Toolkit.Api;

/// <summary>
///     Descriptor for the installed Sims game instance.
/// </summary>
public class GameInstance
{
    /// <summary>
    ///     Initializes an instance of <see cref="GameInstance" />.
    /// </summary>
    /// <param name="path">The installed path of the game.</param>
    /// <param name="platform">The <see cref="PlatformID" />.</param>
    public GameInstance(string path, PlatformID platform)
    {
        GamePath = path;
        InstalledPacks = new PackCollection();
        Platform = platform;
    }

    /// <summary>
    ///     The game installation path.
    /// </summary>
    public string GamePath { get; }

    /// <summary>
    ///     The <see cref="PackCollection" />.
    /// </summary>
    public PackCollection? InstalledPacks { get; }

    /// <summary>
    ///     The platform the game is installed on.
    /// </summary>
    public PlatformID Platform { get; }
}
