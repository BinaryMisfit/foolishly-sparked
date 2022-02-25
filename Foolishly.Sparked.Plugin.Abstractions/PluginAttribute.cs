using System;
using System.ComponentModel.Composition;

namespace Foolishly.Sparked.Plugin;

/// <summary>
///     Implementation of IPluginAttribute.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class PluginAttribute : ExportAttribute, IPluginAttribute
{
    /// <summary>
    ///     Initializes an instance of <see cref="IPluginAttribute" />.
    /// </summary>
    /// <param name="name">The name of the plugin.</param>
    /// <param name="pluginType">The <see cref="PluginType" />.</param>
    /// <param name="platform">The <see cref="PlatformID" /> the plugin applies to.</param>
    public PluginAttribute(string name, PluginType pluginType, PlatformID platform)
        : base(typeof(IPluginEngine))
    {
        Name = name;
        Platform = platform;
        PluginType = pluginType;
    }

    /// <inheritdoc />
    public string Name { get; }

    /// <inheritdoc />
    public PlatformID Platform { get; }

    /// <inheritdoc />
    public PluginType PluginType { get; }
}
