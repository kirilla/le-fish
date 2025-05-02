using System.Net;

namespace Lefish.Web.Middleware;

public class IpFilterMiddleware(RequestDelegate next, IIpRangeCacheService cacheService)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var requestIp = context.Connection.RemoteIpAddress;

        if (requestIp != null && await IsIpBlocked(requestIp))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Access Denied");
            return;
        }

        await next(context);
    }

    private async Task<bool> IsIpBlocked(IPAddress requestIp)
    {
        var ipRanges = await cacheService.GetIpRanges();

        foreach (var range in ipRanges)
        {
            if (range.IsInRange(requestIp))
            {
                return range.Blocked;
            }
        }

        return false;
    }
}
