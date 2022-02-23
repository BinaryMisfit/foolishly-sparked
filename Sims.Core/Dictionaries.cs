namespace Sims.Core;

public static class Dictionaries
{
    public static readonly Dictionary<PackType, string> PackTypeFolders = new()
    {
        {PackType.BaseGame, "Client"},
        {PackType.Expansion, "EP"},
        {PackType.Free, "FP"},
        {PackType.Game, "GP"},
        {PackType.Stuff, "SP"}
    };

    public static readonly IDictionary<PackType, string> PackTypeName = new Dictionary<PackType, string>
    {
        {PackType.BaseGame, "Base Game"},
        {PackType.Expansion, "Expansion Pack"},
        {PackType.Free, "Free Stuff"},
        {PackType.Game, "Game Pack"},
        {PackType.Kit, "Kit"},
        {PackType.Stuff, "Stuff Pack"}
    };
}
