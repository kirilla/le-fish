namespace Lefish.Web.Models;

public class TargetInstructionSummary
{
    public int Id { get; set; }

    public string Name { get; set; }
    
    public int Reference { get; set; }

    public DateTime? Created { get; set; }

    public InstructionStatus InstructionStatus { get; set; }
}
