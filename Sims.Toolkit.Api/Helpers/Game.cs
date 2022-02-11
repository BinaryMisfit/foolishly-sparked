using System;
using System.Composition.Hosting;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Sims.Toolkit.Api.Core;
using Sims.Toolkit.Api.Interfaces;

namespace Sims.Toolkit.Api.Helpers;

/// <summary>
///     Contains and stores game specific information.
/// </summary>
[SuppressMessage("Major Code Smell", "S3885:\"Assembly.Load\" should be used")]
public sealed class Game : IGame
{
    private readonly IFileSystem _fileSystem;

    public Game(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public IPlatform LoadPlugin()
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
}
