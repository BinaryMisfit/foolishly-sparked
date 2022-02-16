using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using Sims.Toolkit.Api.Core;
using Sims.Toolkit.Api.Core.Interfaces;
using Sims.Toolkit.Api.Enums;
using Sims.Toolkit.Api.Helpers.Interfaces;
using Sims.Toolkit.Api.Plugin.Attributes.Interfaces;
using Sims.Toolkit.Api.Plugin.Interfaces;
using Sims.Toolkit.Api.Plugin.Properties;

namespace Sims.Toolkit.Api.Helpers;

/// <summary>
///     Contains and stores platform specific information.
/// </summary>
public sealed class GameLoader : IGameLoader
{
    private readonly IFileSystem _fileSystem;

    [ImportMany(typeof(ICoreApiPlugin))] private IEnumerable<Lazy<ICoreApiPlugin, IExportPlatformAttribute>>? services;

    public GameLoader(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
        LoadPlatformPlugin();
    }

    public async Task<IGameInstance> LoadGameAsync()
    {
        return await LoadGameAsync(null);
    }

    public async Task<IGameInstance> LoadGameAsync(IProgress<ProgressReport>? progress)
    {
        var service =
            services?.FirstOrDefault(plugin =>
                plugin.Metadata.Platform == Environment.OSVersion.Platform);
        var plugin = (IPlatform) service?.Value;
        if (plugin == null)
        {
            throw new DllNotFoundException("Platform plugin not found.");
        }

        await plugin.LocateGameAsync();
        var installedPath = plugin.InstalledPath;
        var platform = plugin.Platform;
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
                gamePacks.Add(new Pack(packFile.Directory.Name)
                {
                    Path = packFile.Directory
                });
            }
        });

        var loadPacks = new List<Task<IPack>>();
        gamePacks.ToList().ForEach(pack => { loadPacks.Add(LoadPackAsync(pack, progress)); });
        await Task.WhenAll(loadPacks);
        var game = new GameInstance(installedPath, platform);
        return game;
    }

    private Task<IPack> LoadPackAsync(IPack pack, IProgress<ProgressReport>? progress)
    {
        var packFiles =
            _fileSystem.Directory.GetFiles(pack.Path?.FullName, Constants.ClientFiles, SearchOption.TopDirectoryOnly);
        if (!packFiles.Any())
        {
            return Task.FromResult(pack);
        }

        packFiles.ToList().ForEach(path =>
        {
            progress?.Report(new ProgressReport($"Loading File: {path}"));
            var package = new Package(_fileSystem);
            package.LoadFromFile(path);
            package.LoadPackageAsync(progress);
            package.LoadPackageContentAsync(progress);
            package.IsReadOnly = true;
            pack.Contents.Add(package);
        });

        return Task.FromResult(pack);
    }

    private void LoadPlatformPlugin()
    {
        var catalog = new DirectoryCatalog("Core", "Sims.Toolkit.Api.Plugin.Core.Platform.dll");
        var container = new CompositionContainer(catalog);
        container.ComposeParts(this);
    }
}
