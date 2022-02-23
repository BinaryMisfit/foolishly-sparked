using System.IO.Abstractions;
using Microsoft.Extensions.Options;
using Sims.Core;
using Sims.Core.Properties;

namespace Sims.Api.Game;

/// <summary>
///     Instance of the installed Sims game.
/// </summary>
public class GameInstance : IGameLocator
{
    private readonly IFileSystem _fileSystem;
    private readonly GameInstanceOptions? _options;

    /// <summary>
    ///     Initializes an instance of <see cref="GameInstance" />.
    /// </summary>
    public GameInstance(IFileSystem fileSystem, IOptions<GameInstanceOptions> options)
    {
        _fileSystem = fileSystem;
        _options = options.Value;
    }

    /// <summary>
    ///     The game installation path.
    /// </summary>
    public string? GamePath { get; private set; }

    /// <summary>
    ///     The <see cref="PackCollection" />.
    /// </summary>
    public IPackCollection? InstalledPacks { get; private set; }

    /// <summary>
    ///     The platform the game is installed on.
    /// </summary>
    public PlatformID Platform { get; }

    /// <summary>
    ///     Locates the current game installation.
    /// </summary>
    /// <returns>The <see cref="IGameInstance" />.</returns>
    public IGameInstance LocateGame()
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

    /// <summary>
    ///     Locates all the game packs installed for this game instance.
    /// </summary>
    /// <returns>The <see cref="IGameInstance" />.</returns>
    public IGameInstance LocateGamePacks()
    {
        InstalledPacks = new PackCollection();
        var directories = _fileSystem.DirectoryInfo.FromDirectoryName(GamePath)
            .GetDirectories("*", SearchOption.AllDirectories)
            .Where(current => !Constants.IgnoreGameFolders.Contains(current.Parent.Name))
            .OrderBy(current => current.Name);
        foreach (var directory in directories)
        {
            var files = directory.GetFiles(Constants.FilesClientPackage, SearchOption.TopDirectoryOnly);
            if (!files.Any())
            {
                continue;
            }

            var pack = new PackDescriptor(directory.Name);
            InstalledPacks.Add(pack);
        }

        return this;
    }
}
