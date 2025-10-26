using OracleBlazor.Application.Interfaces;

namespace OracleBlazor.Api.Middleware;

public class IpWhitelistMiddleware
{
    private readonly RequestDelegate _next;

    public IpWhitelistMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context, IAllowedIpsRepository repo)
    {
        var ip = GetClientIp(context);
        var list = await repo.GetAllowedIpsAsync(); 
        var allowed = list.Any(x=> x.Ip == ip); 

        if (!allowed)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Forbidden: IP not allowed.");
            return;
        }

        await _next(context);
    }

    private static string GetClientIp(HttpContext ctx)
    {
        var fwd = ctx.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrWhiteSpace(fwd))
            return fwd.Split(',').First().Trim();

        return ctx.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
    }
}
