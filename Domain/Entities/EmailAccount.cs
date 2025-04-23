namespace Lefish.Domain.Entities;

public class EmailAccount
{
    public int Id { get; set; }

    public string FromName { get; set; }
    public string FromAddress { get; set; }

    public string? ReplyToName { get; set; }
    public string? ReplyToAddress { get; set; }

    public string Password { get; set; }
    
    public string SmtpHost { get; set; }
    public int SmtpPort { get; set; }

    public List<EmailMessage> EmailMessages { get; set; }
}
