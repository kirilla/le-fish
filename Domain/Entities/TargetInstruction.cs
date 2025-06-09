namespace Lefish.Domain.Entities;

public class TargetInstruction : ICreatedDateTime, IFormatOnSave
{
    public int Id { get; set; }

    public string Name { get; set; }
    public string Script { get; set; }

    public int Reference { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Fetched { get; set; }

    public InstructionStatus InstructionStatus { get; set; }

    public Attack Attack { get; set; }
    public int AttackId { get; set; }

    public void FormatOnSave()
    {
        if (Reference == 0)
        {
            Reference = Random.Shared.Next();
        }
    }
}
