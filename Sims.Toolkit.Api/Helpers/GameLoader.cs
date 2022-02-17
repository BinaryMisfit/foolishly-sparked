using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
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
using Sims.Toolkit.Api.Plugin.Interfaces.Meta;
using Sims.Toolkit.Api.Plugin.Interfaces.Shared;
using Sims.Toolkit.Api.Plugin.Properties;

namespace Sims.Toolkit.Api.Helpers;

/// <summary>
///     Implementation of the <see cref="IGameLoader" /> interface.
/// </summary>
public sealed class GameLoader : IGameLoader
{
    private readonly IFileSystem _fileSystem;

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
            throw new DirectoryNotFoundException(Exceptions.GameBaseNotFound);
        }

        var game = new GameInstance(installedPath, platform);
        game.InstalledPacks.LoadPacks(PackType.BaseGame, gamePackSources, progress)
            .LoadPacks(PackType.Game, gamePackSources, progress)
            .LoadPacks(PackType.Expansion, gamePackSources, progress)
            .LoadPacks(PackType.Stuff, gamePackSources, progress)
            .LoadPacks(PackType.Free, gamePackSources, progress);
        return game;
    }

    private void LoadPlatformPlugin()
    {
        var catalog = new DirectoryCatalog(Constants.DirectoryApiCore, Constants.FilePlatformPlugin);
        var container = new CompositionContainer(catalog);
        container.ComposeParts(this);
        services = container.GetExports<ICoreApiPlugin, IExportPlatformAttribute>();
    }
}
