namespace Sims.Toolkit.Api.Core;

internal static class Constants
{
    internal const string headerBit = "DBPF";

    internal const int fields = 9;
    internal const int indexCount = 36;
    internal const int indexPosition = 64;
    internal const int indexPositionAlternate = 40;
    internal const int indexSize = 44;
    internal const int majorStart = 4;
    internal const int minorStart = 8;
    internal const int packageMajor = 2;
    internal const int packageMinor = 1;

    internal static readonly byte[] headerId = new byte[96];
}
