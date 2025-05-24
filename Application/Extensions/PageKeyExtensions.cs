namespace Lefish.Application.Extensions;

public static class PageKeyExtensions
{
    public static async Task SetUniqueTokenAsync(this PageKey key, IDatabaseService database)
    {
        var tokens = database.PageKeys
            .Select(x => x.Value)
            .ToList();

        do
        {
            key.Value = Random.Shared.Next();
        }
        while (tokens.Contains(key.Value));
    }
}
