namespace Lefish.Application.Extensions;

public static class EmailMessageExtensions
{
    public static void InsertTargetValues(this EmailMessage message, EmailTarget target)
    {
        message.Subject = message.Subject.Replace("[[target_name]]", target.Name);
        message.Subject = message.Subject.Replace("[[target_address]]", target.Address);

        message.HtmlBody = message.HtmlBody.Replace("[[target_name]]", target.Name);
        message.HtmlBody = message.HtmlBody.Replace("[[target_address]]", target.Address);

        message.TextBody = message.TextBody.Replace("[[target_name]]", target.Name);
        message.TextBody = message.TextBody.Replace("[[target_address]]", target.Address);
    }
}
