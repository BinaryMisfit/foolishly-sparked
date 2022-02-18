using System;
using System.IO;
using Sims.Toolkit.Plugin;

namespace Sims.Toolkit.Api.Plugin.Platform;

/// <summary>
///     Plugin to locate the game installation on OSX.
/// </summary>
[Plugin("GameLoader", PluginType.Core, PlatformID.MacOSX)]
public class AppleGameLoader : IToolkitPlugin
{
    private const string GlobalPath = "/Applications/The Sims 4.app";

    private readonly string _userPath = Path.Join(
        Environment.GetEnvironmentVariable("HOME"),
        "/Applications/The Sims 4.app");

    /// <inheritdoc />
    public void Execute()
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
    }

    /// <inheritdoc />
    public void Register()
    {
        // This plugin does not require registration.
    }
}
