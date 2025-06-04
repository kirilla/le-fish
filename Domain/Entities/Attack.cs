namespace Lefish.Domain.Entities;

public class Attack : ICreatedDateTime
{
    public int Id { get; set; }

    public int Value { get; set; }

    public DateTime? Created { get; set; }

    public int EmailTargetId { get; set; }
    public int PayloadPageId { get; set; }
    public int PayloadScriptId { get; set; }

    public EmailTarget EmailTarget { get; set; }
    public PayloadPage PayloadPage { get; set; }
    public PayloadScript PayloadScript { get; set; }

    public List<DataDump> DataDumps { get; set; }
    public List<EmailMessage> EmailMessages { get; set; }
    public List<TargetInstruction> TargetInstructions { get; set; }
    public List<Visit> Visits { get; set; }
}
