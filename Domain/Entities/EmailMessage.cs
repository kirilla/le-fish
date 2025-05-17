using Lefish.Domain.Enums;

namespace Lefish.Domain.Entities;

public class EmailMessage : ICreatedDateTime
{
    public int Id { get; set; }

    public string Subject { get; set; }

    public string HtmlBody { get; set; }
    public string TextBody { get; set; }

    public EmailStatus EmailStatus { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Sent { get; set; }

    public int EmailAccountId { get; set; }
    public int EmailTargetId { get; set; }

    public EmailAccount? EmailAccount { get; set; }
    public EmailTarget? EmailTarget { get; set; }

    public List<PhishingToken> PhishingTokens { get; set; }
}
