using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Linq;

public class IpRestrictionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<IpRestrictionMiddleware> _logger;
    // IP de la escuela, localhost (IPv4 e IPv6) y Render
    private readonly string[] _allowedIps = new[] { "187.155.101.200", "127.0.0.1", "::1", "172.68.174.230" };

    public IpRestrictionMiddleware(RequestDelegate next, ILogger<IpRestrictionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var remoteIp = context.Connection.RemoteIpAddress?.ToString();
        var forwardedFor = context.Request.Headers["X-Forwarded-For"].ToString();
        
        _logger.LogInformation($"IP remota: {remoteIp}, ForwardedFor: {forwardedFor}");
        
        bool permitido = false;
        
        // Verificar IP directa
        foreach (var ip in _allowedIps)
        {
            if (remoteIp == ip)
            {
                permitido = true;
                break;
            }
        }
        
        // Verificar en X-Forwarded-For
        if (!permitido && !string.IsNullOrEmpty(forwardedFor))
        {
            var forwardedIps = forwardedFor.Split(',').Select(x => x.Trim()).ToArray();
            foreach (var ip in _allowedIps)
            {
                if (forwardedIps.Contains(ip))
                {
                    permitido = true;
                    break;
                }
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