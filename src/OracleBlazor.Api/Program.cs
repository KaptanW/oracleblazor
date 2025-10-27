using OracleBlazor.Infrastructure;
using OracleBlazor.Application;
using OracleBlazor.Api.Middleware;
using OracleBlazor.Api.Builder;
using Serilog;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddOpenApi();
        builder.Services.AddApplication(builder.Configuration);
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwagger();
        builder.AddJwt();
        builder.AddCors();
        builder.AddLogger();

        builder.Services.AddAuthorization();
        builder.Services.AddControllers();

        var app = builder.Build();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "OracleBlazor API v1");
        });

        app.UseHttpsRedirection();
        app.UseCors("AllowAll");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.UseSerilogRequestLogging();
        app.UseMiddleware<IpWhitelistMiddleware>();
        app.UseMiddleware<App.Middleware.GlobalExceptionHandlerMiddleware>();


        app.Run();
    }
}
