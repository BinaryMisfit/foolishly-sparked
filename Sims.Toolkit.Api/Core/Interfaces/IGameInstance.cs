using JetBrains.Annotations;

namespace Sims.Toolkit.Api.Core.Interfaces;

/// <summary>
///     Represents an instance of the Sims game retrieved from the local installation.
/// </summary>
[PublicAPI]
public interface IGameInstance
{
    /// <summary>
    ///     Gets the installation path of the game.
    /// </summary>
    string GamePath { get; }

    /// <summary>
    ///     Gets the installed packs for the game.
    /// </summary>
    PackCollection InstalledPacks { get; }

    /// <summary>
    ///     Gets the platform the game is installed on.
    /// </summary>
    string Platform { get; }
}
