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
        message.Subject = message.Subject.Replace("[[page_url]]", templateConfiguration.PageUrl);
        message.Subject = message.Subject.Replace("[[target_name]]", target.Name);
        message.Subject = message.Subject.Replace("[[target_address]]", target.Address);
        message.Subject = message.Subject.Replace("[[page_token]]", token.Token.ToString());

        message.HtmlBody = message.HtmlBody.Replace("[[page_url]]", templateConfiguration.PageUrl);
        message.HtmlBody = message.HtmlBody.Replace("[[target_name]]", target.Name);
        message.HtmlBody = message.HtmlBody.Replace("[[target_address]]", target.Address);
        message.HtmlBody = message.HtmlBody.Replace("[[page_token]]", token.Token.ToString());

        message.TextBody = message.TextBody.Replace("[[page_url]]", templateConfiguration.PageUrl);
        message.TextBody = message.TextBody.Replace("[[target_name]]", target.Name);
        message.TextBody = message.TextBody.Replace("[[target_address]]", target.Address);
        message.TextBody = message.TextBody.Replace("[[page_token]]", token.Token.ToString());
    }
}
