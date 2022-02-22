using System.ComponentModel.DataAnnotations;

namespace Sims.Toolkit.Config;

/// <summary>
///     Defines options for the game instance.
/// </summary>
public class GameInstanceOptions
{
    /// <summary>
    ///     The section name in the config file.
    /// </summary>
    public const string ConfigurationSectionName = "GameInstance";

    /// <summary>
    ///     The game installation path.
    /// </summary>
    [Required]
    public string InstallPath { get; set; }

    /// <summary>
    ///     The game content path.
    /// </summary>
    [Required]
    public string ContentPath { get; set; }
}
