using Lefish.Application.Auth;
using Lefish.Common.Settings;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net;

namespace Lefish.Web.Middleware;

public class IpFilterMiddleware(
    RequestDelegate next,
    IDatabaseService database,
    IIpRangeCacheService cacheService,
    IOptions<IpFilterConfiguration> ipFilterOptions)
{
    private readonly IpFilterConfiguration _config = ipFilterOptions.Value;

    public async Task InvokeAsync(HttpContext context)
    {
        var requestIp = context.Connection.RemoteIpAddress;

        if (requestIp != null && await IsIpBlocked(requestIp))
        {
            //context.Response.StatusCode = StatusCodes.Status403Forbidden;
            //await context.Response.WriteAsync("Access Denied");

            context.Response.StatusCode = StatusCodes.Status404NotFound;

            if (_config.LogBlockedRequest)
            {
                var request = new BlockedRequest()
                {
                    Url = context.Request.GetDisplayUrl(),
                    Method = context.Request.Method,
                    IpAddress = context.Connection.RemoteIpAddress?.ToString(),
                    UserAgent = context.Request.Headers?.UserAgent,
                };

                database.BlockedRequests.Add(request);

                await database.SaveAsync(new NoUserToken());
            }

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

        return _config.BlockByDefault;

        // Or simply: return false/true;
    }
}
