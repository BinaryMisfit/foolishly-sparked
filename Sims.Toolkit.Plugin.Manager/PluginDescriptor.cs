using System.Diagnostics;
using Sims.Toolkit.Plugin.Enums;

namespace Sims.Toolkit.Plugin.Manager;

/// <summary>
///     Describes a plugin.
/// </summary>
[DebuggerDisplay("PluginType = {PluginType}")]
public class PluginDescriptor
{
    /// <summary>
    ///     Initializes a new <see cref="PluginDescriptor" /> with the specified <see cref="Enums.PluginType" />
    /// </summary>
    /// <param name="pluginType">The <see cref="PluginType" /> the plugin represents.</param>
    public PluginDescriptor(PluginType pluginType)
    {
        PluginType = pluginType;
    }

    /// <summary>
    ///     The <see cref="PluginType" /> this plugin represents.
    /// </summary>
    public PluginType PluginType { get; }
}
