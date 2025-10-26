using OracleBlazor.Infrastructure;
using OracleBlazor.Application;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using OracleBlazor.Core.Auth;
using OracleBlazor.Api.Middleware;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddApplication(builder.Configuration);
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddEndpointsApiExplorer();   // Minimal API için gerekli
        
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "OracleBlazor API",
                Version = "v1",
                Description = "OracleAssets API"
            });

            // JWT Bearer için "Authorize" düğmesi
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT token'ınızı 'Bearer {token}' formatında girin."
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme, Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
            });
        });

        builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt"));

        builder.Services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwt = builder.Configuration.GetSection("Jwt").Get<JwtSettings>()!;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
                    ClockSkew = TimeSpan.Zero // sürede toleransı kapatmak istersen
                };
            });
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        var app = builder.Build();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            // V1 endpoint’i
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "OracleBlazor API v1");
            // c.RoutePrefix = string.Empty; // İsterseniz Swagger UI'ı kök URL'ye alın (/)
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
app.UseMiddleware<IpWhitelistMiddleware>();

        app.Run();
    }
}
