using System.Collections.Generic;

namespace Sims.Toolkit.Api;

internal static class Dictionaries
{
    internal static readonly IDictionary<PackType, string> PackTypeFolders = new Dictionary<PackType, string>
    {
        {PackType.BaseGame, "Client"},
        {PackType.Expansion, "EP"},
        {PackType.Free, "FP"},
        {PackType.Game, "GP"},
        {PackType.Stuff, "SP"}
    };

    internal static readonly IDictionary<PackType, string> PackTypeName = new Dictionary<PackType, string>
    {
        {PackType.BaseGame, "Base Game"},
        {PackType.Expansion, "Expansion Pack"},
        {PackType.Free, "Free Stuff"},
        {PackType.Game, "Game Pack"},
        {PackType.Kit, "Kit"},
        {PackType.Stuff, "Stuff Pack"}
    };
}
