using System.Composition.Hosting;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using Sims.Toolkit.Api.Core;
using Sims.Toolkit.Api.Interfaces;

namespace Sims.Toolkit.Api.Helpers;

/// <summary>
///     Contains and stores game specific information.
/// </summary>
[SuppressMessage("Major Code Smell", "S3885:\"Assembly.Load\" should be used")]
public static class Game
{
    public static IPlatform LoadPlugin()
    {
        var configuration = new ContainerConfiguration();
        Assembly? assembly = null;
        FileInfo? assemblyFile = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            assemblyFile = new FileInfo($"{Constants.PlatformWindows}.dll");
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            assemblyFile = new FileInfo($"{Constants.PlatformMac}.dll");
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
