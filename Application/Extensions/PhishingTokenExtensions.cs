namespace Lefish.Application.Extensions;

public static class PhishingTokenExtensions
{
    public static async Task SetUniqueTokenAsync(this PhishingToken token, IDatabaseService database)
    {
        var tokens = database.PhishingTokens
            .Select(x => x.Token)
            .ToList();

        do
        {
            token.Token = Random.Shared.Next();
        }
        while (tokens.Contains(token.Token));
    }
}
