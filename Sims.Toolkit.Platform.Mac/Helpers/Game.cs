using System.Composition;
using System.Runtime.InteropServices;
using Sims.Toolkit.Api.Interfaces;

namespace Sims.Toolkit.Platform.Mac.Helpers;

/// <summary>
///     Contains and stores game specific information.
/// </summary>
[Export(typeof(IPlatform))]
public class Game : IPlatform
{
    private const string GlobalPath = "/Applications/The Sims 4.app";
    private const string UserPath = "~/Applications/The Sims 4.app";

    public Game()
    {
        Is64 = RuntimeInformation.OSArchitecture.HasFlag(Architecture.X64) ||
               RuntimeInformation.OSArchitecture.HasFlag(Architecture.Arm64);
        Platform = RuntimeInformation.RuntimeIdentifier;
    }

    public bool Is64 { get; }

    public string Platform { get; }

    public string? InstalledPath { get; private set; }

    public Task<DirectoryInfo> LocateGame()
    {
        var gameFile = new FileInfo(GlobalPath);
        if (!gameFile.Exists) gameFile = new FileInfo(UserPath);

        if (gameFile.Directory == null) throw new FileNotFoundException("Cannot locate an installed version of Sims 4");

        if (!gameFile.Directory.Exists) throw new FileNotFoundException("Cannot locate an installed version of Sims 4");

        InstalledPath = gameFile.Directory.Name;
        return Task.FromResult(gameFile.Directory);
    }
}