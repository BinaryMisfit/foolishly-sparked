using System.ComponentModel.DataAnnotations;

namespace Foolishly.Sparked.Core;

/// <summary>
///     Defines options for the game instance.
/// </summary>
public class GameOptions
{
    /// <summary>
    ///     The section name in the config file.
    /// </summary>
    public static string ConfigurationSectionName => "Game";

    /// <summary>
    ///     The game installation path.
    /// </summary>
    [Required]
    public string? InstallPath { get; set; }

    /// <summary>
    ///     The game content path.
    /// </summary>
    [Required]
    public string? ContentPath { get; set; }
}
