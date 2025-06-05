namespace Lefish.Domain.Entities;

public class Attack : ICreatedDateTime
{
    public int Id { get; set; }

    public int Value { get; set; }

    public DateTime? Created { get; set; }

    public int PayloadPageId { get; set; }
    public int PayloadScriptId { get; set; }
    public int TargetId { get; set; }

    public PayloadPage PayloadPage { get; set; }
    public PageScript PayloadScript { get; set; }
    public Target Target { get; set; }

    public List<AttackEvent> AttackEvents { get; set; }
    public List<DataResult> DataResults { get; set; }
    public List<EmailMessage> EmailMessages { get; set; }
    public List<TargetInstruction> TargetInstructions { get; set; }
}
