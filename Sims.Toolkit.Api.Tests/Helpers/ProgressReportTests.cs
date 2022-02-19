using Sims.Toolkit.Api.Core;
using Xunit;

namespace Sims.Toolkit.Api.Tests.Helpers;

[Collection("UI")]
public class ProgressReportTests
{
    private const string message = "This is a message";

    [Fact]
    [Trait("Category", "Basic")]
    public void Accepts_Message()
    {
        var progress = new ProgressReport(message);
        Assert.Matches(message, progress.Message);
    }
}
