using Lefish.Common.Validation;
using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.EmailTargets.AddEmailTarget;

public class AddEmailTargetCommandModel
{
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

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange phishing-nyckel.")]
    [StringLength(
        MaxLengths.Domain.EmailTarget.PersonKey,
        ErrorMessage = "Skriv kortare.")]
    public string PersonKey { get; set; }
}
