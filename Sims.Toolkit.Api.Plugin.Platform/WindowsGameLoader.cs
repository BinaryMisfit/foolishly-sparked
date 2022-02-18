using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.Win32;
using Sims.Toolkit.Plugin;

namespace Sims.Toolkit.Api.Plugin.Platform;

/// <summary>
///     Plugin to locate the game installation on Windows.
/// </summary>
[Plugin("GameLoader", PluginType.Core, PlatformID.Win32NT)]
[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
public class WindowsGameLoader : IToolkitPlugin
{
    private const string RegistryKey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Maxis\\The Sims 4";
    private const string RegistryValue = "Install Dir";

    /// <inheritdoc />
    public void Execute()
    {
        var value = Registry.GetValue(RegistryKey, RegistryValue, string.Empty) as string;
        if (string.IsNullOrEmpty(value))
        {
            throw new FileNotFoundException("Cannot locate an installed version of Sims 4");
        }

        var directory = new DirectoryInfo(value);
        if (!directory.Exists)
        {
            throw new FileNotFoundException("Cannot locate an installed version of Sims 4");
        }
    }

    /// <inheritdoc />
    public void Register()
    {
        // This plugin does not require registration.
    }
}
