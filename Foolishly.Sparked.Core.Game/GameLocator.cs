using System;
using System.IO;
using System.IO.Abstractions;
using Foolishly.Sparked.Core.Properties;
using Microsoft.Extensions.Options;

namespace Foolishly.Sparked.Core;

/// <summary>
///     Instance of the installed Sims game.
/// </summary>
public class GameLocator : IGameLocator
{
    private readonly IFileSystem _fileSystem;
    private readonly GameOptions? _options;

    /// <summary>
    ///     Initializes an instance of <see cref="GameLocator" />.
    /// </summary>
    public GameLocator(IFileSystem fileSystem, IOptions<CoreOptions> options)
    {
        _fileSystem = fileSystem;
        _options = options.Value.Game;
    }

    /// <summary>
    ///     The game installation path.
    /// </summary>
    public string? GamePath { get; private set; }

    /// <summary>
    ///     The platform the game is installed on.
    /// </summary>
    public PlatformID Platform { get; }

    /// <summary>
    ///     Locates the current game installation.
    /// </summary>
    /// <returns>The <see cref="IGameInstance" />.</returns>
    IGameInstance IGameLocator.LocateGame()
    {
        var gamePath = _fileSystem.DirectoryInfo.FromDirectoryName(_options?.InstallPath);
        if (gamePath == null)
        {
            throw new DirectoryNotFoundException(Exceptions.GameDirectoryNotFound);
        }

        if (!gamePath.Exists)
        {
            throw new DirectoryNotFoundException(Exceptions.GameDirectoryNotFound);
        }

        GamePath = gamePath.FullName;
        return this;
    }
}
