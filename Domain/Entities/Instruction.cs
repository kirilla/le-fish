
namespace Lefish.Domain.Entities;

public class Instruction : ICreatedDateTime
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Script { get; set; }

    public DateTime? Created { get; set; }

    public int InstructionSetId { get; set; }
    public InstructionSet InstructionSet { get; set; }
}
