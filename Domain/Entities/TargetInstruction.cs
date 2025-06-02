namespace Lefish.Domain.Entities;

public class TargetInstruction : ICreatedDateTime
{
    public int Id { get; set; }

    public DateTime? Created { get; set; }

    public Instruction Instruction { get; set; }
    public int InstructionId { get; set; }

    public PageKey PageKey { get; set; }
    public int PageKeyId { get; set; }
}
