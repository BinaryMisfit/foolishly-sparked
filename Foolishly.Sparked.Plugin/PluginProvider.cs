﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Foolishly.Sparked.Plugin;

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

    private void DisposeProvider()
    {
        if (!_disposed)
        {
            _pluginDescriptors.Clear();
        }

        _disposed = true;
    }
}
