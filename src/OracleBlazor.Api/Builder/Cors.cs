using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OracleBlazor.Core.Auth;

namespace OracleBlazor.Api.Builder
{
    public static class Cors
    {
        public static void AddCors(this Microsoft.AspNetCore.Builder.WebApplicationBuilder builder)
        {
          builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });
        }
    }
}