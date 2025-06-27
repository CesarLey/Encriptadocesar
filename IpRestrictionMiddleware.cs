using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class IpRestrictionMiddleware
{
    private readonly RequestDelegate _next;
    // IP de la escuela y localhost (IPv4 e IPv6)
    private readonly string[] _allowedIps = new[] { "187.155.101.200", "127.0.0.1", "::1" };

    public IpRestrictionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var remoteIp = context.Connection.RemoteIpAddress?.ToString();
        bool permitido = false;
        foreach (var ip in _allowedIps)
        {
            if (remoteIp == ip)
            {
                permitido = true;
                break;
            }
        }
        if (!permitido)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Acceso denegado: solo se permite el acceso desde la IP de la escuela o localhost.");
            return;
        }
        await _next(context);
    }
} 