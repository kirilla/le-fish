
namespace Lefish.Domain.Entities;

public class Instruction : ICreatedDateTime
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Script { get; set; }

    public DateTime? Created { get; set; }

    public List<QueuedInstruction> QueuedInstructions { get; set; }
}
