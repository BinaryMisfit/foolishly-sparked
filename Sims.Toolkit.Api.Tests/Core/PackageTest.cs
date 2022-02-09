using System.IO;
using Sims.Toolkit.Api.Core;
using Xunit;

namespace Sims.Toolkit.Api.Tests.Core;

public class PackageTest
{
    private const string TestFile = @"C:\Temp\Test.package";

    /// <summary>
    ///     Initialize <see cref="Package" /> with a <see cref="FileInfo" /> instance.
    /// </summary>
    [Fact]
    public void CanInitializedByFileInfo()
    {
        var fileInfo = new FileInfo(TestFile);
        var package = new Package(fileInfo);
        Assert.NotNull(package.PackageFileName);
    }

    /// <summary>
    ///     Initialized <see cref="Package" /> with a <see cref="FileInfo" /> instance returns correct file.
    /// </summary>
    [Fact]
    public void CorrectFileInitializedByFileInfo()
    {
        var fileInfo = new FileInfo(TestFile);
        var package = new Package(fileInfo);
        Assert.Matches(fileInfo.Name, package.PackageFileName);
    }
}
