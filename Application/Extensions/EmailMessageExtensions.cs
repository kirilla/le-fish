using Lefish.Common.Settings;

namespace Lefish.Application.Extensions;

public static class EmailMessageExtensions
{
    public static void InsertTargetValues(
        this EmailMessage message,
        TemplateConfiguration templateConfiguration,
        EmailTarget target, 
        PageToken token)
    {
        message.Subject = message.Subject.Replace(Variables.PAGE_TOKEN, token.Token.ToString());
        message.Subject = message.Subject.Replace(Variables.PAGE_URL, templateConfiguration.PageUrl);
        message.Subject = message.Subject.Replace(Variables.SCRIPT_URL, templateConfiguration.ScriptUrl);
        message.Subject = message.Subject.Replace(Variables.TARGET_ADDRESS, target.Address);
        message.Subject = message.Subject.Replace(Variables.TARGET_NAME, target.Name);
        
        message.HtmlBody = message.HtmlBody.Replace(Variables.PAGE_TOKEN, token.Token.ToString());
        message.HtmlBody = message.HtmlBody.Replace(Variables.PAGE_URL, templateConfiguration.PageUrl);
        message.HtmlBody = message.HtmlBody.Replace(Variables.SCRIPT_URL, templateConfiguration.ScriptUrl);
        message.HtmlBody = message.HtmlBody.Replace(Variables.TARGET_ADDRESS, target.Address);
        message.HtmlBody = message.HtmlBody.Replace(Variables.TARGET_NAME, target.Name);
        
        message.TextBody = message.TextBody.Replace(Variables.PAGE_TOKEN, token.Token.ToString());
        message.TextBody = message.TextBody.Replace(Variables.PAGE_URL, templateConfiguration.PageUrl);
        message.TextBody = message.TextBody.Replace(Variables.SCRIPT_URL, templateConfiguration.ScriptUrl);
        message.TextBody = message.TextBody.Replace(Variables.TARGET_ADDRESS, target.Address);
        message.TextBody = message.TextBody.Replace(Variables.TARGET_NAME, target.Name);
    }
}
