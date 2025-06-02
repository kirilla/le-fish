using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.TargetInstructions.SelectTargetInstruction;

public class SelectTargetInstructionCommandModel
{
    public int PageKeyId { get; set; }

    [Required(ErrorMessage = "Välj en instruktion.")]
    public int? InstructionId { get; set; }
}
