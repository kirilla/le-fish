namespace Lefish.Domain.Entities;

public class EmailTemplate
{
    public int Id { get; set; }

    public string Subject { get; set; }

    public string HtmlBody { get; set; }
    public string TextBody { get; set; }

    public List<EmailAttachment> EmailAttachments { get; set; }
    public List<EmailImage> EmailImages { get; set; }
}
