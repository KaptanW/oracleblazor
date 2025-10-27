using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OracleBlazor.Core.Auth;
using Serilog;
using Serilog.Events;

namespace OracleBlazor.Api.Builder
{
    public static class Serilog
    {
        public static void AddLogger(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error)
                .WriteTo.Console()
                .WriteTo.File("logs/app-.log", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
                .CreateLogger();

            builder.Host.UseSerilog();
        }

    }
}