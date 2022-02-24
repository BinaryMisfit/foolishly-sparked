namespace Foolishly.Sparked.Plugin;

/// <summary>
///     Represents the functional base for the plugin to be executed.
/// </summary>
public interface IPluginEngine
{
    /// <summary>
    ///     Execute the code defined by the plugin.
    /// </summary>
    void Execute();
}
