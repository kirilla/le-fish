namespace Lefish.Common.Settings;

public class IpFilterConfiguration
{
    public bool IsFiltering { get; set; }
    public bool BlockByDefault { get; set; }
    public bool LogBlockedRequest { get; set; }
}
