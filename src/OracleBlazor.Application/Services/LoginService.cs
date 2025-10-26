using System.Security.Claims;
using OracleBlazor.Application.DTOs;
using OracleBlazor.Application.Interfaces;
using OracleBlazor.Core.Entities;
using OracleBlazor.Core.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
namespace OracleBlazor.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly JwtSettings _jwt;

        public UserService(IUserRepository repo, IOptions<JwtSettings> jwtOptions)
        {
            _repo = repo;
            _jwt = jwtOptions.Value;
        }

        public Task<User> Create(CreateDto createDto)
        {
            return _repo.CreateAsync(createDto.Username, createDto.Password, createDto.RealName);
        }

        public async Task<string> Login(LoginDto loginDto)
        {
            var user = await _repo.LoginAsync(loginDto.Username, loginDto.Password);
            if (user is null) return null;
            // user türünü kendi entity’ine göre değiştir
            var claims = new List<System.Security.Claims.Claim>
    {
        new System.Security.Claims.Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new System.Security.Claims.Claim(ClaimTypes.Name, user.UserName),
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_jwt.ExpireMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}