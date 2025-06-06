using Lefish.Common.Settings;

namespace Lefish.Application.Extensions;

public static class PayloadPageExtensions
{
    public static string ReplaceVariables(
        this WebPage page,
        TemplateConfiguration templateConfiguration,
        Target target, 
        Attack attack)
    {
        string s = page.Html;

        // Step 1
        s = s.Replace(Variables.PAGE_URL, templateConfiguration.PageUrl);
        s = s.Replace(Variables.SCRIPT_URL, templateConfiguration.ScriptUrl);

        // Step 2
        s = s.Replace(Variables.ATTACK_TOKEN, attack.Value.ToString());
        s = s.Replace(Variables.TARGET_ADDRESS, target.Address);
        s = s.Replace(Variables.TARGET_NAME, target.Name);

        // Q: Why two steps?
        // A: PAGE_URL and SCRIPT_URL may contain PAGE_TOKEN.

        return s;
    }
}
