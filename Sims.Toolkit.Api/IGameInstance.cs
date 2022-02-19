using System;

namespace Sims.Toolkit.Api;

/// <summary>
///     Represents an installed game instance.
/// </summary>
public interface IGameInstance
{
    /// <summary>
    ///     The game installation path.
    /// </summary>
    string? GamePath { get; }

    /// <summary>
    ///     The <see cref="PackCollection" />.
    /// </summary>
    PackCollection? InstalledPacks { get; }

    /// <summary>
    ///     The platform the game is installed on.
    /// </summary>
    PlatformID Platform { get; }
}
