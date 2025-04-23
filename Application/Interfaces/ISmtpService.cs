namespace Lefish.Application.Interfaces;

public interface ISmtpService
{
    void SendMessage(
        EmailTarget target,
        EmailMessage email,
        EmailAccount account,
        List<EmailAttachment> attachments,
        List<EmailImage> images);
}
