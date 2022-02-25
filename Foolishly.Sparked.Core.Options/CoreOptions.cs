using System.ComponentModel.DataAnnotations;

namespace Foolishly.Sparked.Core;

/// <summary>
///     Defines options sets.
/// </summary>
public class CoreOptions
{
    public static string ConfigurationSectionName => "Core";

    /// <summary>
    ///     The <see cref="GameOptions" />.
    /// </summary>
    [Required]
    public GameOptions? Game { get; set; }
}
