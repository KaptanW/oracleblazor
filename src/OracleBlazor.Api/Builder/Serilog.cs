using Serilog;
using Serilog.Events;
using Serilog.Extensions.Hosting; // DiagnosticContext

public static class LoggingExtensions
{
    public static void AddLogger(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Oracle.EntityFrameworkCore", LogEventLevel.Error)
            .MinimumLevel.Override("Oracle.ManagedDataAccess.Core", LogEventLevel.Error)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("logs/app-.log", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
            .CreateLogger();

        builder.Host.UseSerilog();

        builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);

        builder.Services.AddSingleton<Serilog.IDiagnosticContext, DiagnosticContext>();
    }
}
