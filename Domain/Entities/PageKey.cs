namespace Lefish.Domain.Entities;

public class PageKey : ICreatedDateTime
{
    public int Id { get; set; }

    public int Value { get; set; }

    public DateTime? Created { get; set; }

    public int EmailMessageId { get; set; }
    public int PayloadPageId { get; set; }
    public int PayloadScriptId { get; set; }

    public EmailMessage EmailMessage { get; set; }
    public PayloadPage PayloadPage { get; set; }
    public PayloadScript PayloadScript { get; set; }

    public List<PageVisit> PageVisits { get; set; }
    public List<ScriptVisit> ScriptVisits { get; set; }
}
