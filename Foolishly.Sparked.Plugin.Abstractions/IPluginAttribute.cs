using System;

namespace Foolishly.Sparked.Plugin;

/// <summary>
///     Provides meta data for a plugin.
/// </summary>
public interface IPluginAttribute
{
    /// <summary>
    ///     The name of the plugin.
    /// </summary>
    string Name { get; }

    /// <summary>
    ///     The <see cref="PlatformID" /> the plugin applies to.
    /// </summary>
    PlatformID Platform { get; }

    /// <summary>
    ///     The <see cref="PluginType" />.
    /// </summary>
    PluginType PluginType { get; }
}
