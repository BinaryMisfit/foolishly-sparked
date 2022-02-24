namespace Foolishly.Sparked.Plugin;

/// <summary>
///     Represents a plugin for the Sims Toolkit containing extended functionality.
/// </summary>
public interface IToolkitPlugin : IPluginEngine
{
    /// <summary>
    ///     Register functionality for the toolkit.
    /// </summary>
    void Register();
}
