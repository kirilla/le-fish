using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.Instructions.AddInstruction;

public class AddInstructionCommandModel
{
    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange namn.")]
    [StringLength(
        MaxLengths.Domain.Instruction.Name,
        ErrorMessage = "Skriv kortare.")]
    public string Name { get; set; }

    [RegularExpression(Pattern.Common.AnythingMultiLine)]
    [Required(ErrorMessage = "Skriv ett skript.")]
    [StringLength(
        MaxLengths.Domain.Instruction.Script,
        ErrorMessage = "Skriv kortare.")]
    public string Script { get; set; }
}
