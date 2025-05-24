using Lefish.Common.Settings;

namespace Lefish.Application.Extensions;

public static class PayloadScriptExtensions
{
    public static void InsertTargetValues(
        this PayloadScript script,
        TemplateConfiguration templateConfiguration,
        EmailTarget target, 
        PageKey token)
    {
        // Step 1
        script.Script = script.Script.Replace(Variables.PAGE_URL, templateConfiguration.PageUrl);
        script.Script = script.Script.Replace(Variables.SCRIPT_URL, templateConfiguration.ScriptUrl);

        // Step 2
        script.Script = script.Script.Replace(Variables.PAGE_TOKEN, token.Token.ToString());
        script.Script = script.Script.Replace(Variables.TARGET_ADDRESS, target.Address);
        script.Script = script.Script.Replace(Variables.TARGET_NAME, target.Name);

        // Q: Why two steps?
        // A: PAGE_URL and SCRIPT_URL may contain PAGE_TOKEN.
    }
}
