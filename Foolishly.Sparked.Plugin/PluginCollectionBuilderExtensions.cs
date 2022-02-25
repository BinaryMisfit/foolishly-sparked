using System;
using System.ComponentModel.Composition.Hosting;
using System.IO.Abstractions;
using System.Linq;

namespace Foolishly.Sparked.Plugin;

/// <summary>
///     Extension methods to build a <see cref="PluginProvider" /> from an <see cref="IPluginCollection" />.
/// </summary>
public static class PluginCollectionBuilderExtensions
{
    /// <summary>
    ///     Creates a <see cref="PluginProvider" /> containing plugins from the provided <see cref="IPluginCollection" />.
    /// </summary>
    /// <param name="plugins">The <see cref="IPluginCollection" /> containing plugin descriptors.</param>
    /// <returns>The <see cref="PluginProvider" />.</returns>
    public static PluginProvider BuildPluginProvider(this IPluginCollection plugins)
    {
        return new PluginProvider(plugins);
    }

    /// <summary>
    ///     Populates a <see cref="IPluginCollection" /> from assemblies located in the <see cref="IFileSystem" />.
    /// </summary>
    /// <param name="plugins">The <see cref="IPluginCollection" /> to populate.</param>
    /// <returns>The <see cref="IPluginCollection" />.</returns>
    public static IPluginCollection AddToolkitPlugins(this IPluginCollection plugins)
    {
        return AddToolkitPlugins(plugins, "Core");
    }

    /// <summary>
    ///     Populates a <see cref="IPluginCollection" /> from assemblies located in the <see cref="IFileSystem" />.
    /// </summary>
    /// <param name="plugins">The <see cref="IPluginCollection" /> to populate.</param>
    /// <param name="searchPath">The path to search.</param>
    /// <returns>The <see cref="IPluginCollection" />.</returns>
    public static IPluginCollection AddToolkitPlugins(this IPluginCollection plugins, string searchPath)
    {
        var catalog = new DirectoryCatalog(searchPath, "*.dll");
        var container = new CompositionContainer(catalog);
        var parts = container.GetExports<IPluginEngine>()
            .ToList();
        parts.ForEach(
            part =>
            {
                var descriptor = part.Value.BuildPluginDescriptor();
                plugins.Add(descriptor);
            });
        return plugins;
    }

    private static PluginDescriptor BuildPluginDescriptor(this IPluginEngine plugin)
    {
        var meta = (IPluginAttribute?) Attribute.GetCustomAttribute(plugin.GetType(), typeof(PluginAttribute));
        if (meta == null)
        {
            throw new NotSupportedException("Plugin meta data not provided.");
        }

        return new PluginDescriptor(meta.Name, meta.PluginType, meta.Platform, plugin);
    }
}
