using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.TargetInstructions.CloneTargetInstruction;

public class CloneTargetInstructionCommandModel
{
    public int Id { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange namn.")]
    [StringLength(
        MaxLengths.Domain.TargetInstruction.Name,
        ErrorMessage = "Skriv kortare.")]
    public string Name { get; set; }
}
