using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.Targets.AddTarget;

public class AddTargetCommandModel
{
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
