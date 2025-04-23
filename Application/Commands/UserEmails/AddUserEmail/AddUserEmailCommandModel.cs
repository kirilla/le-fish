using Lefish.Common.Validation;
using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.UserEmails.AddUserEmail;

public class AddUserEmailCommandModel
{
    [RegularExpression(Pattern.Common.Email.Address)]
    [Required(ErrorMessage = "Skriv din epostadress.")]
    [StringLength(
        MaxLengths.Common.Email.Address,
        ErrorMessage = "Skriv kortare.")]
    public string Address { get; set; }
}
