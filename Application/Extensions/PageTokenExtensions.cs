namespace Lefish.Application.Extensions;

public static class PageTokenExtensions
{
    public static async Task SetUniqueTokenAsync(this PageToken token, IDatabaseService database)
    {
        var tokens = database.PageKeys
            .Select(x => x.Token)
            .ToList();

        do
        {
            token.Token = Random.Shared.Next();
        }
        while (tokens.Contains(token.Token));
    }
}
