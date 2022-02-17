using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using Sims.Toolkit.Api.Core;
using Sims.Toolkit.Api.Core.Interfaces;
using Sims.Toolkit.Api.Enums;
using Sims.Toolkit.Api.Extensions;
using Sims.Toolkit.Api.Helpers.Interfaces;
using Sims.Toolkit.Api.Plugin.Attributes.Interfaces;
using Sims.Toolkit.Api.Plugin.Interfaces;
using Sims.Toolkit.Api.Plugin.Properties;

namespace Sims.Toolkit.Api.Helpers;

/// <summary>
///     Implementation of the <see cref="IGameLoader" /> interface.
/// </summary>
public sealed class GameLoader : IGameLoader
{
    private readonly IFileSystem _fileSystem;

    [SuppressMessage("Style", "IDE0044:Add readonly modifier")]
    [ImportMany(typeof(ICoreApiPlugin))]
    private IEnumerable<Lazy<ICoreApiPlugin, IExportPlatformAttribute>>? services;

    /// <summary>
    ///     Initializes an instance of <see cref="GameLoader" />.
    /// </summary>
    /// <param name="fileSystem">Instance of <see cref="IFileSystem" /> provided via DI.</param>
    public GameLoader(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
        LoadPlatformPlugin();
    }

    /// <inheritdoc />
    public async Task<IGameInstance> LoadGameAsync()
    {
        return await LoadGameAsync(null);
    }

    /// <inheritdoc />
    public async Task<IGameInstance> LoadGameAsync(IProgress<ProgressReport>? progress)
    {
        var service = services?.FirstOrDefault(plugin => plugin.Metadata.Platform == Environment.OSVersion.Platform);
        var plugin = (IPlatform) service?.Value;
        if (plugin == null)
        {
            throw new DllNotFoundException(Exceptions.PluginMissingPlatform);
        }

        progress?.Report(
            new ProgressReport(
                string.Format(CultureInfo.CurrentCulture, OutputMessages.PluginLoadedPlatform, plugin.Platform)));
        await plugin.LocateGameAsync();
        var installedPath = plugin.InstalledPath;
        var platform = plugin.Platform;
        var rootPath = _fileSystem.DirectoryInfo.FromDirectoryName(installedPath);
        if (!rootPath.Exists)
        {
            throw new DirectoryNotFoundException(Exceptions.GameDirectoryNotFound);
        }

        var gamePackSources = _fileSystem.DirectoryInfo.FromDirectoryName(rootPath.FullName)
            .GetDirectories("*", SearchOption.AllDirectories);
        if (!gamePackSources.Any())
        {
            throw new DirectoryNotFoundException("Base game not found.");
        }

        var game = new GameInstance(installedPath, platform);
        game.InstalledPacks.LoadPacks(PackType.BaseGame, gamePackSources, progress)
            .LoadPacks(PackType.Game, gamePackSources, progress)
            .LoadPacks(PackType.Expansion, gamePackSources, progress)
            .LoadPacks(PackType.Stuff, gamePackSources, progress)
            .LoadPacks(PackType.Free, gamePackSources, progress);
        return game;
    }

    private Task<IPack> LoadPackAsync(IPack pack, IProgress<ProgressReport>? progress)
    {
        var packFiles = _fileSystem.Directory.GetFiles(
            pack.Path?.FullName,
            Constants.ClientFiles,
            SearchOption.TopDirectoryOnly);
        if (!packFiles.Any())
        {
            return Task.FromResult(pack);
        }

        packFiles.ToList()
            .ForEach(
                path =>
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
