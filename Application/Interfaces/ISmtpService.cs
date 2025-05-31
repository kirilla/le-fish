namespace Lefish.Application.Interfaces;

public interface ISmtpService
{
    void SendMessage(
        EmailMessage email,
        EmailAccount account,
        List<EmailAttachment> attachments,
        List<EmailImage> images);
}
