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
    ///     The executable for the game.
    /// </summary>
    public static readonly string FilesExecutable = "Game\\Bin\\TS4*.exe";

    /// <summary>
    ///     List of folders to ignore during game import.
    /// </summary>
    public static readonly string[] IgnoreGameFolders = {"_Installer", "Game", "Shared", "Soundtrack", "Support"};
}
