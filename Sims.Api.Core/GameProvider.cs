namespace Sims.Api.Core;

/// <summary>
///     The default IGameProvider.
/// </summary>
public sealed class GameProvider : IGameProvider, IDisposable, IAsyncDisposable
{
    private readonly IPackCollection? _packs;
    private bool _disposed;
    private string? _gameDirectory;
    private PlatformID _platformId;

    /// <inheritdoc />
    public ValueTask DisposeAsync()
    {
        if (_disposed)
        {
            return default;
        }

        DisposeProvider();
        return default;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        DisposeProvider();
    }

    /// <inheritdoc />
    public string PrintGameInfo()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(GameInstance));
        }

        return string.Empty;
    }

    private void DisposeProvider()
    {
        if (!_disposed)
        {
            _packs?.Clear();
        }

        _disposed = true;
    }
}
