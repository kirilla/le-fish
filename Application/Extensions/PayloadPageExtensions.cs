namespace Lefish.Application.Extensions;

public static class PayloadPageExtensions
{
    public static void InsertTargetValues(this PayloadPage page, EmailTarget target)
    {
        page.Html = page.Html.Replace("[[target_name]]", target.Name);
        page.Html = page.Html.Replace("[[target_address]]", target.Address);
        page.Html = page.Html.Replace("[[target_key]]", target.PersonKey);
    }
}
