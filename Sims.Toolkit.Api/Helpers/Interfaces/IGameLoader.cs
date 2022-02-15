using System;
using System.Threading.Tasks;
using Sims.Toolkit.Api.Core.Interfaces;
using Sims.Toolkit.Api.Plugin.Interfaces;

namespace Sims.Toolkit.Api.Helpers.Interfaces;

public interface IGameLoader
{
    IPlatform LoadPlatformPlugin();

    Task<IGameInstance> LoadGameAsync(string installedPath, string platform);

    Task<IGameInstance> LoadGameAsync(string installedPath, string platform, IProgress<ProgressReport>? progress);
}
