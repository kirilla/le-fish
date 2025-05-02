namespace Lefish.Application.Interfaces;

public interface IIpRangeCacheService
{
    Task<List<IpRange>> GetIpRanges();
    
    void InvalidateCache();
}
