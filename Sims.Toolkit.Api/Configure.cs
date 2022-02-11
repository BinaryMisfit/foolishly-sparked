using System.IO.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Sims.Toolkit.Api.Core;
using Sims.Toolkit.Api.Helpers;
using Sims.Toolkit.Api.Interfaces;

namespace Sims.Toolkit.Api;

public static class Configure
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IFileSystem, FileSystem>();
        services.AddScoped<IGame, Game>();
        services.AddScoped<IPackage, Package>();
    }
}
