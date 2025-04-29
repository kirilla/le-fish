namespace Lefish.Application.Extensions;

public static class PayloadPageExtensions
{
    public static void InsertTargetValues(this PayloadPage page, EmailTarget target)
    {
        page.Html = page.Html.Replace("[[target:id]]", target.PersonKey?.ToString());
    }
}
