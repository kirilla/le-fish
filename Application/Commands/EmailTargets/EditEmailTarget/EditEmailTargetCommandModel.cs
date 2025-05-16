using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.EmailTargets.EditEmailTarget;

public class EditEmailTargetCommandModel
{
    public int EmailTargetId { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange namn.")]
    [StringLength(
        MaxLengths.Domain.EmailTarget.Name,
        ErrorMessage = "Skriv kortare.")]
    public string Name { get; set; }

    [RegularExpression(Pattern.Common.Email.Address)]
    [Required(ErrorMessage = "Ange epostadress.")]
    [StringLength(
        MaxLengths.Domain.EmailTarget.Address,
        ErrorMessage = "Skriv kortare.")]
    public string Address { get; set; }
}
