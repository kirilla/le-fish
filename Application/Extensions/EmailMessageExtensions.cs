using Lefish.Common.Settings;

namespace Lefish.Application.Extensions;

public static class EmailMessageExtensions
{
    public static void InsertTargetValues(
        this EmailMessage message,
        TemplateConfiguration templateConfiguration,
        EmailTarget target, 
        PageKey key)
    {
        // Step 1
        message.Subject = message.Subject.Replace(Variables.PAGE_URL, templateConfiguration.PageUrl);
        message.HtmlBody = message.HtmlBody.Replace(Variables.PAGE_URL, templateConfiguration.PageUrl);
        message.TextBody = message.TextBody.Replace(Variables.PAGE_URL, templateConfiguration.PageUrl);

        message.Subject = message.Subject.Replace(Variables.SCRIPT_URL, templateConfiguration.ScriptUrl);
        message.HtmlBody = message.HtmlBody.Replace(Variables.SCRIPT_URL, templateConfiguration.ScriptUrl);
        message.TextBody = message.TextBody.Replace(Variables.SCRIPT_URL, templateConfiguration.ScriptUrl);

        // Step 2
        message.Subject = message.Subject.Replace(Variables.PAGE_KEY, key.Value.ToString());
        message.Subject = message.Subject.Replace(Variables.TARGET_ADDRESS, target.Address);
        message.Subject = message.Subject.Replace(Variables.TARGET_NAME, target.Name);
        
        message.HtmlBody = message.HtmlBody.Replace(Variables.PAGE_KEY, key.Value.ToString());
        message.HtmlBody = message.HtmlBody.Replace(Variables.TARGET_ADDRESS, target.Address);
        message.HtmlBody = message.HtmlBody.Replace(Variables.TARGET_NAME, target.Name);
        
        message.TextBody = message.TextBody.Replace(Variables.PAGE_KEY, key.Value.ToString());
        message.TextBody = message.TextBody.Replace(Variables.TARGET_ADDRESS, target.Address);
        message.TextBody = message.TextBody.Replace(Variables.TARGET_NAME, target.Name);

        // Q: Why two steps?
        // A: PAGE_URL and SCRIPT_URL may contain PAGE_TOKEN.
    }
}
