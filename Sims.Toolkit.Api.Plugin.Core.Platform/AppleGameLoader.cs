using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Sims.Toolkit.Api.Plugin.Attributes;
using Sims.Toolkit.Api.Plugin.Interfaces;

namespace Sims.Toolkit.Api.Plugin.Core.Platform;

[ExportPlatform(PlatformID.MacOSX)]
[PublicAPI]
public class AppleGameLoader : ICoreApiPlugin, IPlatform
{
    private const string GlobalPath = "/Applications/The Sims 4.app";

    private readonly string _userPath =
        Path.Join(Environment.GetEnvironmentVariable("HOME"), "/Applications/The Sims 4.app");

    public AppleGameLoader()
    {
        Is64 = RuntimeInformation.OSArchitecture.HasFlag(Architecture.X64) ||
               RuntimeInformation.OSArchitecture.HasFlag(Architecture.Arm64);
        Platform = RuntimeInformation.RuntimeIdentifier;
        InstalledPath = GlobalPath;
    }

    public bool Is64 { get; }

    public string Platform { get; }

    public string InstalledPath { get; private set; }

    public Task<IPlatform> LocateGameAsync()
    {
        var gameFile = new FileInfo(GlobalPath);
        if (!gameFile.Exists)
        {
            gameFile = new FileInfo(_userPath);
        }

        if (gameFile.Directory == null)
        {
            throw new FileNotFoundException("Cannot locate an installed version of Sims.");
        }

        if (!gameFile.Directory.Exists)
        {
            throw new FileNotFoundException("Cannot locate an installed version of Sims.");
        }

        InstalledPath = gameFile.Directory.FullName;
        return Task.FromResult((IPlatform) this);
    }
}
