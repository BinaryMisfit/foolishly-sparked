using Microsoft.Extensions.Configuration;

namespace Foolishly.Sparked.Core;

public interface IGameBuilder
{
    IServiceProvider CreateProvider();

    IGameBuilder UseConfiguration(IConfiguration configuration);
}
