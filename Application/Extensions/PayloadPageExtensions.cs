namespace Lefish.Application.Extensions;

public static class PayloadPageExtensions
{
    public static void InsertTargetValues(this PayloadPage page, EmailTarget target)
    {
        page.Html = page.Html.Replace("[[target_key]]", target.PersonKey);
    }

    public static void GeneratePhishingKey(this PayloadPage page, List<string> takenKeys)
    {
        string? key = null;

        while (key == null)
        {
            key = Random.Shared.Next().ToString();

            if (takenKeys.Any(x => x == key))
                key = null;
        }

        page.PageKey = key;
    }
}
