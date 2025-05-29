namespace Lefish.Web.Models;

public class QueuedInstructionPlus
{
    public int Id { get; set; }

    public int InstructionId { get; set; }

    public DateTime? Created { get; set; }

    public string Name { get; set; }
}
