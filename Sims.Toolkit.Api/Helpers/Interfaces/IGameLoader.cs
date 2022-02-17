using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Sims.Toolkit.Api.Core.Interfaces;

namespace Sims.Toolkit.Api.Helpers.Interfaces;

/// <summary>
///     Represents a set of functions to load and locate the installed instance of The Sims.
/// </summary>
[PublicAPI]
public interface IGameLoader
{
    /// <summary>
    ///     Load the game instance information asynchronously.
    /// </summary>
    /// <returns>A populated <see cref="IGameInstance" />.</returns>
    Task<IGameInstance> LoadGameAsync();

    /// <summary>
    ///     Load the game instance information asynchronously.
    /// </summary>
    /// <param name="progress">Instance of a <see cref="IProgress{T}" /> for progress reporting.</param>
    /// <returns>A populated <see cref="IGameInstance" />.</returns>
    Task<IGameInstance> LoadGameAsync(IProgress<ProgressReport>? progress);
}
