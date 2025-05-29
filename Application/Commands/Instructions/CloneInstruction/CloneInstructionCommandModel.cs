using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.Instructions.CloneInstruction;

public class CloneInstructionCommandModel
{
    public int InstructionId { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange namn.")]
    [StringLength(
        MaxLengths.Domain.Instruction.Name,
        ErrorMessage = "Skriv kortare.")]
    public string Name { get; set; }
}
