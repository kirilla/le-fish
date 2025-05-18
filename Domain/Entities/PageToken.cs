namespace Lefish.Domain.Entities;

public class PageToken : ICreatedDateTime
{
    public int Id { get; set; }

    public int Token { get; set; }

    public DateTime? Created { get; set; }

    public int EmailMessageId { get; set; }
    public int PayloadPageId { get; set; }

    public EmailMessage EmailMessage { get; set; }
    public PayloadPage PayloadPage { get; set; }
}
