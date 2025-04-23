using Lefish.Common.Validation;
using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.Companies.AddCompany;

public class AddCompanyCommandModel
{
    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Vad heter företaget?")]
    [StringLength(
        MaxLengths.Common.Company.Name,
        ErrorMessage = "Skriv kortare.")]
    public string Name { get; set; }
}
