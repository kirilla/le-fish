using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.TargetInstructions.AddTargetInstruction;

public class AddTargetInstructionCommandModel
{
    public int PageKeyId { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange namn.")]
    [StringLength(
        MaxLengths.Domain.TargetInstruction.Name,
        ErrorMessage = "Skriv kortare.")]
    public string Name { get; set; }

    [RegularExpression(Pattern.Common.AnythingMultiLine)]
    [Required(ErrorMessage = "Skriv ett skript.")]
    [StringLength(
        MaxLengths.Domain.TargetInstruction.Script,
        ErrorMessage = "Skriv kortare.")]
    public string Script { get; set; }
}
