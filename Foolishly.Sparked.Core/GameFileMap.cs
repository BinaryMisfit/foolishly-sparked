namespace Foolishly.Sparked.Core;

/// <summary>
///     Contains static values for the game files.
/// </summary>
public static class GameFileMap
{
    /// <summary>
    ///     The client package file for a game pack.
    /// </summary>
    public static readonly string FilesClientPackage = "Client*Build0.package";

    /// <summary>
    ///     List of folders to ignore during game import.
    /// </summary>
    public static readonly string[] IgnoreGameFolders = {"_Installer", "Delta", "Game", "Shared", "Soundtrack", "Support"};
}
