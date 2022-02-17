namespace Sims.Toolkit.Api.Enums;

internal static class Constants
{
    internal const string ClientFiles = "Client*Build0.package";

    internal const string HeaderBit = "DBPF";

    internal const int ContentCount = 36;
    internal const int ContentPosition = 64;
    internal const int ContentPositionAlternate = 40;
    internal const int Fields = 9;
    internal const int InstanceStart = 12;
    internal const int InstanceStartAlternate = 16;
    internal const int MajorStart = 4;
    internal const int MinorStart = 8;
    internal const int PackageMajor = 2;
    internal const int PackageMinor = 1;
    internal const int ResourceGroupStart = 8;
    internal const int ResourceTypeStart = 4;

    internal static readonly byte[] HeaderId = new byte[96];
    internal static readonly string[] IgnoreGameFolders = {"_Installer", "Delta", "Game", "Shared", "Soundtrack", "Support"};
}
