using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.Win32;
using Sims.Toolkit.Api.Core;
using Sims.Toolkit.Config;
using Sims.Toolkit.Plugin.Properties;

namespace Sims.Toolkit.Api;

/// <summary>
///     Instance of the installed Sims game.
/// </summary>
public class GameInstance : IGameLocator
{
    private readonly IFileSystem _fileSystem;
    private readonly GameInstanceOptions _options;

    /// <summary>
    ///     Initializes an instance of <see cref="GameInstance" />.
    /// </summary>
    public GameInstance(IFileSystem fileSystem, IOptions<GameInstanceOptions> options)
    {
        _fileSystem = fileSystem;
        if (options.Value != null)
        {
            _options = options.Value;
        }
    }

    /// <summary>
    ///     The game installation path.
    /// </summary>
    public string? GamePath { get; private set; }

    /// <summary>
    ///     The <see cref="PackCollection" />.
    /// </summary>
    public PackCollection? InstalledPacks { get; private set; }

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
        return LocateApple(Platform);
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

    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
    private IGameInstance LocateWindows()
    {
        const string registryKey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Maxis\\The Sims 4";
        const string registryValue = "Install Dir";
        var value = Registry.GetValue(registryKey, registryValue, string.Empty) as string;
        if (string.IsNullOrEmpty(value))
        {
            throw new FileNotFoundException(Exceptions.GameDirectoryNotFound);
        }

        var gameDirectory = _fileSystem.DirectoryInfo.FromDirectoryName(value);
        if (!gameDirectory.Exists)
        {
            throw new DirectoryNotFoundException(Exceptions.GameDirectoryNotFound);
        }

        GamePath = gameDirectory.FullName;
        return this;
    }

    private IGameInstance LocateApple(PlatformID platform)
    {
        var gamePath = _fileSystem.DirectoryInfo.FromDirectoryName(_options.GameInstallPath);
        if (platform == PlatformID.MacOSX)
        {
            const string GlobalPath = "/Applications/The Sims 4.app";
            var userPath = Path.Join(Environment.GetEnvironmentVariable("HOME"), "/Applications/The Sims 4.app");
            gamePath = _fileSystem.DirectoryInfo.FromDirectoryName(GlobalPath);
            if (!gamePath.Exists)
            {
                gamePath = _fileSystem.DirectoryInfo.FromDirectoryName(userPath);
            }
        }

        if (platform == PlatformID.Win32NT)
        {
            const string searchPattern = "TS4*.exe";
            var drives = _fileSystem.DriveInfo.GetDrives();
            foreach (var drive in drives)
            {
                var files = drive.RootDirectory.GetFiles(searchPattern, SearchOption.AllDirectories);
                if (files.Any())
                {
                    break;
                }
            }
        }

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
