using System.IO.Abstractions;
using JetBrains.Annotations;
using Sims.Toolkit.Api.Assets.Properties;
using Sims.Toolkit.Api.Core.Interfaces;
using Sims.Toolkit.Api.Enums;

namespace Sims.Toolkit.Api.Core;

[PublicAPI]
public class Pack : IPack
{
    public Pack(string packId)
    {
        PackId = packId;
        PackName = GamePacks.ResourceManager.GetString(packId) ?? GamePacks.EP00;
        PackTypeId = DetermineTypeId();
        PackType = DetermineType();
        Contents = new PackContent();
    }

    public string PackId { get; }

    public string? PackName { get; }

    public PackType PackType { get; }

    public int PackTypeId { get; }

    public IDirectoryInfo? Path { get; set; }

    public IPackContent Contents { get; set; }

    private int DetermineTypeId()
    {
        var type = PackId[2..] ?? string.Empty;
        if (int.TryParse(type, out var id))
        {
            return id;
        }

        return -1;
    }

    private PackType DetermineType()
    {
        var type = PackId[..2] ?? string.Empty;
        if (string.IsNullOrEmpty(type))
        {
            return PackType.Error;
        }

        return type.ToUpperInvariant() switch
        {
            Constants.PackBase => PackType.BaseGame,
            Constants.PackExpansion => PackType.Expansion,
            Constants.PackFree => PackType.Free,
            Constants.PackGame => PackType.Game,
            Constants.PackStuff => PackTypeId < 20 ? PackType.Stuff : PackType.Kit,
            var _ => PackType.Unknown
        };
    }
}
