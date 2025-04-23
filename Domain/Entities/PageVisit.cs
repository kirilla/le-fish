namespace Lefish.Domain.Entities;

public class PageVisit : ICreatedDateTime
{
    public int Id { get; set; }

    public DateTime? Created { get; set; }

    public string? Url { get; set; }
    public string? Method { get; set; }

    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    
    public PayloadPage PayloadPage { get; set; }
    public int PayloadPageId { get; set; }

    public EmailTarget EmailTarget { get; set; }
    public int EmailTargetId { get; set; }
}
