using Lefish.Common.Settings;

namespace Lefish.Application.Extensions;

public static class PayloadPageExtensions
{
    public static void InsertTargetValues(
        this PayloadPage page,
        TemplateConfiguration templateConfiguration,
        EmailTarget target, 
        PageToken token)
    {
        // Step 1
        page.Html = page.Html.Replace(Variables.PAGE_URL, templateConfiguration.PageUrl);
        page.Html = page.Html.Replace(Variables.SCRIPT_URL, templateConfiguration.ScriptUrl);

        // Step 2
        page.Html = page.Html.Replace(Variables.PAGE_TOKEN, token.Token.ToString());
        page.Html = page.Html.Replace(Variables.TARGET_ADDRESS, target.Address);
        page.Html = page.Html.Replace(Variables.TARGET_NAME, target.Name);

        // Q: Why two steps?
        // A: PAGE_URL and SCRIPT_URL may contain PAGE_TOKEN.
    }
}
