using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.TargetInstructions.SelectTargetInstruction;

public class SelectTargetInstructionCommandModel
{
    public int AttackId { get; set; }

    [Required(ErrorMessage = "Välj en instruktion.")]
    public int? InstructionId { get; set; }
}
