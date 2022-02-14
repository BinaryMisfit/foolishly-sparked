using Sims.Toolkit.Api.Enums;

namespace Sims.Toolkit.Api.Core.Interfaces;

public interface IPack
{
    string PackId { get; }

    string PackName { get; }

    PackType PackType { get; }

    int PackTypeId { get; }
}
