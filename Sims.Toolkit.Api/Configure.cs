using System.IO.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Sims.Toolkit.Api.Core;
using Sims.Toolkit.Api.Core.Interfaces;
using Sims.Toolkit.Api.Helpers;
using Sims.Toolkit.Api.Helpers.Interfaces;

namespace Sims.Toolkit.Api;

public static class Configure
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IFileSystem, FileSystem>();
        services.AddScoped<IGameLoader, GameLoader>();
        services.AddScoped<IPackage, Package>();
    }
}
