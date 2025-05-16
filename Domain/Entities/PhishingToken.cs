namespace Lefish.Domain.Entities;

public class PhishingToken : ICreatedDateTime
{
    public int Id { get; set; }

    public int Token { get; set; }

    public DateTime? Created { get; set; }

    public int EmailTargetId { get; set; }
    public int PayloadPageId { get; set; }

    public EmailTarget EmailTarget { get; set; }
    public PayloadPage PayloadPage { get; set; }
}
