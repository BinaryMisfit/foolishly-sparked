using Sims.Toolkit.Api.Core.Interfaces;
using Sims.Toolkit.Api.Plugin.Interfaces;

namespace Sims.Toolkit.Api.Helpers.Interfaces;

public interface IGameLoader
{
    IPlatform LoadPlatformPlugin();

    IGame LoadGame(string installedPath, string platform);
}
