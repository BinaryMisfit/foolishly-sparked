using System;
using Xunit;

namespace Sims.Toolkit.Api.Tests.Core;

[Collection("Game")]
public class GameInstanceTests
{
    private const string gamePath = @"C:\Game\Installed";
    private const PlatformID gamePlatform = PlatformID.Win32NT;

    [Fact]
    [Trait("Category", "Basic")]
    public void Accepts_Path()
    {
        var gameInstance = new GameInstance(gamePath, gamePlatform);
        Assert.Equal(gamePath, gameInstance.GamePath);
    }

    [Fact]
    [Trait("Category", "Basic")]
    public void Accepts_Platform()
    {
        var gameInstance = new GameInstance(gamePath, gamePlatform);
        Assert.Equal(gamePlatform, gameInstance.Platform);
    }

    [Fact]
    [Trait("Category", "Initialization")]
    public void Initializes_Installed_Packs()
    {
        var gameInstance = new GameInstance(gamePath, gamePlatform);
        Assert.Empty(gameInstance.InstalledPacks);
    }
}
