using System;
using System.Composition.Hosting;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Sims.Toolkit.Api.Assets.Properties;
using Sims.Toolkit.Api.Core;
using Sims.Toolkit.Api.Core.Interfaces;
using Sims.Toolkit.Api.Enums;
using Sims.Toolkit.Api.Helpers.Interfaces;
using Sims.Toolkit.Api.Plugin.Interfaces;

namespace Sims.Toolkit.Api.Helpers;

/// <summary>
///     Contains and stores platform specific information.
/// </summary>
[SuppressMessage("Major Code Smell", "S3885:\"Assembly.Load\" should be used")]
public sealed class GameLoaderLoader : IGameLoader
{
    private readonly IFileSystem _fileSystem;

    public GameLoaderLoader(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public IPlatform LoadPlatformPlugin()
    {
        var configuration = new ContainerConfiguration();
        Assembly? assembly = null;
        IFileInfo? assemblyFile = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            assemblyFile = _fileSystem.FileInfo.FromFileName($"{Constants.PlatformWindows}.dll");
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            assemblyFile = _fileSystem.FileInfo.FromFileName($"{Constants.PlatformMac}.dll");
        }

        if (assemblyFile == null)
        {
            throw new FileNotFoundException($"Missing assembly for {RuntimeInformation.RuntimeIdentifier}");
        }

        if (assemblyFile.Exists)
        {
            assembly = Assembly.LoadFrom(assemblyFile.FullName);
        }

        if (assembly == null)
        {
            throw new DllNotFoundException($"Missing target platform {RuntimeInformation.RuntimeIdentifier}");
        }

        configuration.WithAssembly(assembly);
        var host = configuration.CreateContainer();
        var game = host.GetExports<IPlatform>().FirstOrDefault();
        if (game == null)
        {
            throw new EntryPointNotFoundException("No matching IPlatform interfaces found.");
        }

        return game;
    }

    public IGame LoadGame(string installedPath, string platform)
    {
        var rootPath = _fileSystem.DirectoryInfo.FromDirectoryName(installedPath);
        if (!rootPath.Exists)
        {
            throw new DirectoryNotFoundException(Exceptions.GameDirectoryNotFound);
        }

        var gamePacks = new PackCollection();
        var gameFolders = _fileSystem.Directory.GetDirectories(rootPath.FullName, string.Empty, SearchOption.AllDirectories);
        gameFolders.ToList().ForEach(path =>
        {
            var packFiles = _fileSystem.Directory.GetFiles(path, Constants.ClientFiles, SearchOption.TopDirectoryOnly);
            if (!packFiles.Any())
            {
                return;
            }

            var packFile = _fileSystem.FileInfo.FromFileName(packFiles.First());
            if (!Constants.IgnoreGameFolders.Contains(packFile.Directory.Name) &&
                !gamePacks.Any(pack => pack.PackId.Equals(packFile.Directory.Name, StringComparison.InvariantCulture)) &&
                !Constants.IgnoreGameFolders.Contains(packFile.Directory.Parent.Name))
            {
                gamePacks.Add(new Pack(packFile.Directory.Name));
            }
        });

        var gameData = rootPath.GetFiles(Constants.ClientFiles, SearchOption.AllDirectories);
        if (!gameData.Any())
        {
            throw new FileNotFoundException(Exceptions.GameFilesNotFound, installedPath);
        }

        var game = new Game(installedPath, platform, gamePacks);
        return game;
    }
}
