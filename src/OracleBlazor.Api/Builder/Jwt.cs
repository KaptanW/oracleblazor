using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OracleBlazor.Core.Auth;

namespace OracleBlazor.Api.Builder
{
    public static class JWT
    {
        public static void AddJwt(this Microsoft.AspNetCore.Builder.WebApplicationBuilder builder)
        {
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
                        ClockSkew = TimeSpan.Zero
                    };
                });

        }
    }
}