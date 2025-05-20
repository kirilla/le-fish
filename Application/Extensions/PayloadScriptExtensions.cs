using Lefish.Common.Settings;

namespace Lefish.Application.Extensions;

public static class PayloadScriptExtensions
{
    public static void InsertTargetValues(
        this PayloadScript script,
        TemplateConfiguration templateConfiguration,
        EmailTarget target, 
        PageToken token)
    {
        script.Script = script.Script.Replace(Variables.PAGE_URL, templateConfiguration.PageUrl);
        script.Script = script.Script.Replace(Variables.TARGET_NAME, target.Name);
        script.Script = script.Script.Replace(Variables.TARGET_ADDRESS, target.Address);
        script.Script = script.Script.Replace(Variables.PAGE_TOKEN, token.Token.ToString());
    }
}
