using Microsoft.AspNetCore.Http;
using OracleBlazor.Application.Interfaces;

namespace OracleBlazor.Infrastructure.Repositories;

public sealed class HttpContextCurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _http;

    public HttpContextCurrentUser(IHttpContextAccessor http) => _http = http;

    public string? UserId
    {
        get
        {
            var ctx = _http.HttpContext;
            if (ctx is null) return null;

            var claimId = ctx.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrWhiteSpace(claimId)) return claimId;

            if (ctx.Request.Headers.TryGetValue("X-User-Id", out var headerId) && !string.IsNullOrWhiteSpace(headerId))
                return headerId.ToString();

            return null;
        }
    }
}

public sealed class NullCurrentUser : ICurrentUser
{
    public string? UserId => "MIGRATION";
}