using System.IO;
using Sims.Toolkit.Api.Core;
using Xunit;

namespace Sims.Toolkit.Api.Tests.Core;

public class PackageTest
{
    private const string TestFile = @"C:\Temp";

    [Fact]
    public void CanInitializeWithFileInfo()
    {
        var fileInfo = new FileInfo(TestFile);
        var package = new Package(fileInfo);
        Assert.NotNull(package.PackageFileName);
    }
}
