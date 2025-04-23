using System.ComponentModel.DataAnnotations;
using Lefish.Common.Validation;

namespace Lefish.Application.Commands.Companies.EditCompany;

public class EditCompanyCommandModel
{
    public int CompanyId { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Vad heter företaget?")]
    [StringLength(
        MaxLengths.Common.Company.Name,
        ErrorMessage = "Skriv kortare.")]
    public string Name { get; set; }
}
