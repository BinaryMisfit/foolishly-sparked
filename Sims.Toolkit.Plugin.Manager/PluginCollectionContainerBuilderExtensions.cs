using System.IO.Abstractions;

namespace Sims.Toolkit.Plugin.Manager;

/// <summary>
///     Extension methods to build a <see cref="PluginProvider" /> from an <see cref="IPluginCollection" />.
/// </summary>
public static class PluginCollectionContainerBuilderExtensions
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
        return AddToolkitPlugins(plugins, new FileSystem(), "Core");
    }

    /// <summary>
    ///     Populates a <see cref="IPluginCollection" /> from assemblies located in the <see cref="IFileSystem" />.
    /// </summary>
    /// <param name="plugins">The <see cref="IPluginCollection" /> to populate.</param>
    /// <param name="fileSystem">The <see cref="IFileSystem" /> containing the assemblies.</param>
    /// <returns>The <see cref="IPluginCollection" />.</returns>
    public static IPluginCollection AddToolkitPlugins(this IPluginCollection plugins, IFileSystem fileSystem)
    {
        return AddToolkitPlugins(plugins, fileSystem, "Core");
    }

    /// <summary>
    ///     Populates a <see cref="IPluginCollection" /> from assemblies located in the <see cref="IFileSystem" />.
    /// </summary>
    /// <param name="plugins">The <see cref="IPluginCollection" /> to populate.</param>
    /// <param name="fileSystem">The <see cref="IFileSystem" /> containing the assemblies.</param>
    /// <param name="searchPath">The path to search for plugins.</param>
    /// <returns>The <see cref="IPluginCollection" />.</returns>
    public static IPluginCollection AddToolkitPlugins(
        this IPluginCollection plugins,
        IFileSystem fileSystem,
        string searchPath)
    {
        return plugins;
    }
}
