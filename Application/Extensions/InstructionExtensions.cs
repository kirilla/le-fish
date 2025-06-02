using Lefish.Common.Settings;

namespace Lefish.Application.Extensions;

public static class InstructionExtensions
{
    public static void InsertTargetValues(
        this Instruction script,
        TemplateConfiguration templateConfiguration,
        EmailTarget target, 
        Attack attack)
    {
        // Step 1
        script.Script = script.Script.Replace(Variables.PAGE_URL, templateConfiguration.PageUrl);
        script.Script = script.Script.Replace(Variables.SCRIPT_URL, templateConfiguration.ScriptUrl);

        // Step 2
        script.Script = script.Script.Replace(Variables.ATTACK_TOKEN, attack.Value.ToString());
        script.Script = script.Script.Replace(Variables.TARGET_ADDRESS, target.Address);
        script.Script = script.Script.Replace(Variables.TARGET_NAME, target.Name);

        // Q: Why two steps?
        // A: PAGE_URL and SCRIPT_URL may contain PAGE_TOKEN.
    }
}
