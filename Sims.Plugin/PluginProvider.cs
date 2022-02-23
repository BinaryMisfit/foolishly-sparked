using System.Globalization;

namespace Sims.Plugin;

/// <summary>
///     The default IPluginProvider
/// </summary>
public sealed class PluginProvider : IPluginProvider, IDisposable, IAsyncDisposable
{
    private readonly ICollection<PluginDescriptor> _pluginDescriptors;
    private bool _disposed;

    internal PluginProvider(ICollection<PluginDescriptor> pluginDescriptors)
    {
        _pluginDescriptors = pluginDescriptors;
    }

    /// <inheritdoc />
    public ValueTask DisposeAsync()
    {
        DisposeProvider();
        return default;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (!_disposed)
        {
            DisposeProvider();
        }
    }

    /// <inheritdoc />
    public IPluginEngine? GetPlugin(string Name, PlatformID platformId)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(IPluginEngine));
        }

        return _pluginDescriptors.FirstOrDefault(plugin => plugin.Platform == platformId && plugin.Name == Name)
            ?.Plugin;
    }

    /// <inheritdoc />
    public IEnumerable<string> GetPluginList()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(IPluginEngine));
        }

        return _pluginDescriptors.Select(
                plugin => string.Format(
                    CultureInfo.CurrentCulture,
                    "{0}\t{1}\t{2}",
                    plugin.PluginType,
                    plugin.Platform,
                    plugin.Name))
            .ToList();
    }

    private void DisposeProvider()
    {
        if (!_disposed)
        {
            _pluginDescriptors.Clear();
        }

        _disposed = true;
    }
}
