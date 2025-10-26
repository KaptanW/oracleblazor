using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OracleBlazor.Application.Interfaces;
using OracleBlazor.Application.Services;
namespace OracleBlazor.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAllowedIpsService, AllowedIpsService>();
        return services;
    }
}


// buraya başla