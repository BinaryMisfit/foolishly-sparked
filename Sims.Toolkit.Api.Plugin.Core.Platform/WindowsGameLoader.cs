using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Win32;
using Sims.Toolkit.Api.Plugin.Attributes;
using Sims.Toolkit.Api.Plugin.Interfaces;

namespace Sims.Toolkit.Api.Plugin.Core.Platform;

[ExportPlatform(PlatformID.Win32NT)]
[PublicAPI]
[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
public class WindowsGameLoader : ICoreApiPlugin, IPlatform
{
    private const string RegistryKey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Maxis\\The Sims 4";
    private const string RegistryValue = "Install Dir";

    public WindowsGameLoader()
    {
        Is64 = RuntimeInformation.OSArchitecture.HasFlag(Architecture.X64);
        Platform = RuntimeInformation.RuntimeIdentifier;
        InstalledPath = string.Empty;
    }

    public bool Is64 { get; }

    public string Platform { get; }

    public string InstalledPath { get; private set; }

    public Task<IPlatform> LocateGameAsync()
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

        InstalledPath = directory.FullName;
        return Task.FromResult((IPlatform) this);
    }
}
