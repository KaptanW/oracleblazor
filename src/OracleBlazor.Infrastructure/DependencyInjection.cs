// Infrastructure/DependencyInjection.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oracle.EntityFrameworkCore; // Oracle EF Core provider
using OracleBlazor.Application.Interfaces;
using OracleBlazor.Infrastructure.Persistence;
using OracleBlazor.Infrastructure.Repositories;
namespace OracleBlazor.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var cs = config.GetConnectionString("Oracle")
                 ?? throw new InvalidOperationException("OracleDb connection string missing.");

        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseOracle(cs, o =>
            {
             });
        });
services.AddScoped<IAssetRepository, AssetRepository>();
    services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
    services.AddHttpContextAccessor();
services.AddScoped<ICurrentUser, HttpContextCurrentUser>();
;
services.AddScoped<IAllowedIpsRepository, AllowedIpsRepository>();
        return services;
    }
}
