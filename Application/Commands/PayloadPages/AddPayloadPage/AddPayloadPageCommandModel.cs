using Lefish.Common.Validation;
using System.ComponentModel.DataAnnotations;

namespace Lefish.Application.Commands.PayloadPages.AddPayloadPage;

public class AddPayloadPageCommandModel
{
    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange namn.")]
    [StringLength(
        MaxLengths.Domain.PayloadPage.Name,
        ErrorMessage = "Skriv kortare.")]
    public string Name { get; set; }

    [RegularExpression(Pattern.Common.AnythingMultiLine)]
    [Required(ErrorMessage = "Skriv HTML.")]
    [StringLength(
        MaxLengths.Domain.PayloadPage.Html,
        ErrorMessage = "Skriv kortare.")]
    public string Html { get; set; }
}
