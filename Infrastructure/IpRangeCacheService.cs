using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Lefish.Application.Interfaces;

namespace Lefish.Infrastructure;

public class IpRangeCacheService(
    IDatabaseService database,
    IMemoryCache cache) : IIpRangeCacheService
{
    private const string cacheKey = "allowedIpRanges";

    public async Task<List<IpRange>> GetIpRanges()
    {
        var list = new List<IpRange>();

        if (cache.TryGetValue(cacheKey, out list))
            return list ?? new List<IpRange>();

        list = await database.IpRanges
            .AsNoTracking()
            .ToListAsync();

        list = list
            .OrderByDescending(x => x.Prefix) // Higher specificity
            .ThenBy(x => x.BaseAddress)
            .ToList();

        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
        };

        cache.Set(cacheKey, list, options);

        return list;
    }

    public void InvalidateCache()
    {
        cache.Remove(cacheKey);
    }
}
