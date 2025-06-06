using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.InstructionSets.EditInstructionSet;

public class EditInstructionSetCommandModel
{
    public int Id { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange ämnesrad.")]
    [StringLength(
        MaxLengths.Domain.InstructionSet.Name,
        ErrorMessage = "Skriv kortare.")]
    public string Name { get; set; }
}
