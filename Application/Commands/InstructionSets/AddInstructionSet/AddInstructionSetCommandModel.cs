using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.InstructionSets.AddInstructionSet;

public class AddInstructionSetCommandModel
{
    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange ämnesrad.")]
    [StringLength(
        MaxLengths.Domain.InstructionSet.Name,
        ErrorMessage = "Skriv kortare.")]
    public string Name { get; set; }
}
