using System.Threading.Tasks;

namespace Sims.Toolkit.Api.Plugin.Interfaces;

public interface IPlatform
{
    bool Is64 { get; }

    string Platform { get; }

    string InstalledPath { get; }

    Task<IPlatform> LocateGameAsync();
}
