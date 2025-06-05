using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.Targets.EditTarget;

public class EditTargetCommandModel
{
    public int Id { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange namn.")]
    [StringLength(
        MaxLengths.Domain.Target.Name,
        ErrorMessage = "Skriv kortare.")]
    public string Name { get; set; }

    [RegularExpression(Pattern.Common.Email.Address)]
    [Required(ErrorMessage = "Ange epostadress.")]
    [StringLength(
        MaxLengths.Domain.Target.Address,
        ErrorMessage = "Skriv kortare.")]
    public string Address { get; set; }
}
