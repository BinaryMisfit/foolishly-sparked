using System.Collections.Generic;

namespace Foolishly.Sparked.Core;

public static class PackTypesMappings
{
    public static readonly IReadOnlyDictionary<PackTypes, string> PackTypeFolders = new Dictionary<PackTypes, string>
    {
        {PackTypes.BaseGame, "Client"},
        {PackTypes.Expansion, "EP"},
        {PackTypes.Free, "FP"},
        {PackTypes.Game, "GP"},
        {PackTypes.Stuff, "SP"}
    };

    public static readonly IReadOnlyDictionary<PackTypes, string> PackTypeName = new Dictionary<PackTypes, string>
    {
        {PackTypes.BaseGame, "Base Game"},
        {PackTypes.Expansion, "Expansion Pack"},
        {PackTypes.Free, "Free Stuff"},
        {PackTypes.Game, "Game Pack"},
        {PackTypes.Kit, "Kit"},
        {PackTypes.Stuff, "Stuff Pack"}
    };
}
