namespace Lefish.Domain.Entities;

public class Attack : ICreatedDateTime
{
    public int Id { get; set; }

    public int Value { get; set; }

    public DateTime? Created { get; set; }

    public int PageScriptId { get; set; }
    public int TargetId { get; set; }
    public int WebPageId { get; set; }

    public PageScript PageScript { get; set; }
    public Target Target { get; set; }
    public WebPage WebPage { get; set; }

    public List<AttackEvent> AttackEvents { get; set; }
    public List<DataResult> DataResults { get; set; }
    public List<EmailMessage> EmailMessages { get; set; }
    public List<TargetInstruction> TargetInstructions { get; set; }
}
