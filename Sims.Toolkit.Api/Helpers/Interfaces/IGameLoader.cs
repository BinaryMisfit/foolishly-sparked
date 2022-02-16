using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Sims.Toolkit.Api.Core.Interfaces;

namespace Sims.Toolkit.Api.Helpers.Interfaces;

[PublicAPI]
public interface IGameLoader
{
    Task<IGameInstance> LoadGameAsync();

    Task<IGameInstance> LoadGameAsync(IProgress<ProgressReport>? progress);
}
