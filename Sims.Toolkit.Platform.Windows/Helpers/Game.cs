using System.Composition;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Sims.Toolkit.Api.Interfaces;

namespace Sims.Toolkit.Platform.Windows.Helpers;

/// <summary>
///     Contains and stores game specific information.
/// </summary>
[Export(typeof(IPlatform))]
public class Game : IPlatform
{
    private const string registryKey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Maxis\\The Sims 4";
    private const string registryValue = "Install Dir";

    public Game()
    {
        Is64 = RuntimeInformation.OSArchitecture.HasFlag(Architecture.X64);
        Platform = RuntimeInformation.RuntimeIdentifier;
    }

    public bool Is64 { get; }

    public string Platform { get; }

    public string? InstalledPath { get; private set; }

    public Task<DirectoryInfo> LocateGame()
    {
        var value =
            Registry.GetValue(registryKey, registryValue, string.Empty) as string;
        if (string.IsNullOrEmpty(value))
            throw new FileNotFoundException("Cannot locate an installed version of Sims 4");

        var directory = new DirectoryInfo(value);
        if (!directory.Exists) throw new FileNotFoundException("Cannot locate an installed version of Sims 4");

        InstalledPath = directory.FullName;
        return Task.FromResult(directory);
    }
}