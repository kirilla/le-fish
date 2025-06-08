namespace Lefish.Web.Models;

public class InstructionSummary
{
    public int Id { get; set; }

    public int InstructionSetId { get; set; }

    public string Name { get; set; }

    public DateTime? Created { get; set; }
}
