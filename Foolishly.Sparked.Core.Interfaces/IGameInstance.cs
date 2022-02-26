using System;

namespace Foolishly.Sparked.Core;

/// <summary>
///     Represents an installed game instance.
/// </summary>
public interface IGameInstance
{
    /// <summary>
    ///     The game installation path.
    /// </summary>
    string InstallPath { get; }

    /// <summary>
    ///     The platform the game is installed on.
    /// </summary>
    PlatformID Platform { get; }

    string Version { get; }
}
