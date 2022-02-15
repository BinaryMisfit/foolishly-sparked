using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
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
public sealed class GameLoader : IGameLoader
{
    private readonly IFileSystem _fileSystem;

    public GameLoader(IFileSystem fileSystem)
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
            throw new FileNotFoundException(Exceptions.PluginMissingPlatform);
        }

        if (assemblyFile.Exists)
        {
            assembly = Assembly.LoadFrom(assemblyFile.FullName);
        }

        if (assembly == null)
        {
            throw new DllNotFoundException(Exceptions.PluginMissingPlatform);
        }

        configuration.WithAssembly(assembly);
        var host = configuration.CreateContainer();
        var game = host.GetExports<IPlatform>().FirstOrDefault();
        if (game == null)
        {
            throw new EntryPointNotFoundException(Exceptions.PluginInvalid);
        }

        return game;
    }

    public async Task<IGameInstance> LoadGameAsync(string installedPath, string platform)
    {
        return await LoadGameAsync(installedPath, platform, null);
    }

    public async Task<IGameInstance> LoadGameAsync(string installedPath, string platform,
        IProgress<ProgressReport>? progress)
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
            package.Contents.Summary().ToList()
                .ForEach(item =>
                    progress?.Report(new ProgressReport(string.Format(CultureInfo.CurrentCulture,
                        ConsoleOutput.PrintKeyValue, item.Key, item.Value))));
        });

        return Task.FromResult(pack);
    }
}
