using System;
using System.Diagnostics;

namespace Foolishly.Sparked.Plugin;

/// <summary>
///     Describes a plugin.
/// </summary>
[DebuggerDisplay("PluginType = {PluginType}")]
public class PluginDescriptor
{
    /// <summary>
    ///     Initializes a new <see cref="PluginDescriptor" /> with the specified <see cref="PluginType" />
    /// </summary>
    /// <param name="name">The name of the plugin.</param>
    /// <param name="pluginType">The <see cref="PluginType" /> the plugin represents.</param>
    /// <param name="platform">The <see cref="PlatformID" /> the plugin applies to.</param>
    /// <param name="plugin">The <see cref="IPluginEngine" />.</param>
    public PluginDescriptor(string name, PluginType pluginType, PlatformID platform, IPluginEngine plugin)
    {
        Name = name;
        Platform = platform;
        Plugin = plugin;
        PluginType = pluginType;
    }

    /// <summary>
    ///     The name of the plugin.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     The <see cref="PlatformID" /> this plugin applies too.
    /// </summary>
    public PlatformID Platform { get; }

    /// <summary>
    ///     The <see cref="IPluginEngine" />.
    /// </summary>
    public IPluginEngine Plugin { get; }

    /// <summary>
    ///     The <see cref="PluginType" /> this plugin represents.
    /// </summary>
    public PluginType PluginType { get; }
}
