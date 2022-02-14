using Sims.Toolkit.Api.Plugin.Interfaces;

namespace Sims.Toolkit.Api.Helpers.Interfaces;

public interface IGameLoader
{
    IPlatform LoadPlugin();

    void LoadPacks(IPlatform game);
}
