using Microsoft.OpenApi.Models;

namespace OracleBlazor.Api.Builder
{
    public static class Swagger
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
                services.AddSwaggerGen(c =>
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

            return services;
        }
    }
}