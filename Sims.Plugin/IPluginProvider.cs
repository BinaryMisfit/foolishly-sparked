namespace Sims.Plugin;

/// <summary>
///     Specifies the contract for a plugin provider.
/// </summary>
public interface IPluginProvider
{
    /// <summary>
    ///     Returns a plugin from an <see cref="IPluginCollection" /> located by the plugin name.
    /// </summary>
    /// <param name="Name">The name of the plugin</param>
    /// <param name="platformId">The <see cref="PlatformID" /> of the required plugin.</param>
    /// <returns>The <see cref="IPluginEngine" />.</returns>
    IPluginEngine? GetPlugin(string Name, PlatformID platformId);

    /// <summary>
    ///     Returns a string list of all the <see cref="IPluginEngine" /> contained in the provider.
    /// </summary>
    /// <returns>The <see cref="IList{T}" /> of plugin names.</returns>
    IEnumerable<string> GetPluginList();
}
