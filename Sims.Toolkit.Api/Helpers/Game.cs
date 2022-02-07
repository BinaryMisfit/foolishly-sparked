using System.Runtime.InteropServices;

namespace Sims.Toolkit.Api.Helpers;

/// <summary>
///     Contains and stores game specific information.
/// </summary>
public class Game
{
    public Game()
    {
        Is64 = RuntimeInformation.OSArchitecture.HasFlag(Architecture.X64) ||
               RuntimeInformation.OSArchitecture.HasFlag(Architecture.Arm64);
        IsMac = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        IsWin = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        Platform = RuntimeInformation.OSDescription;
    }

    public bool Is64 { get; }

    public bool IsMac { get; }

    public bool IsWin { get; }

    public string Platform { get; }

    public Task<DirectoryInfo> LocateGame()
    {
        throw new NotImplementedException();
    }
}
