using System.ComponentModel.DataAnnotations;
using Lefish.Common.Validation;

namespace Lefish.Application.Commands.PayloadPages.EditPayloadPage;

public class EditPayloadPageCommandModel
{
    public int PayloadPageId { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange namn.")]
    [StringLength(
        MaxLengths.Domain.PayloadPage.Name,
        ErrorMessage = "Skriv kortare.")]
    public string Name { get; set; }

	[Required(ErrorMessage = "Ange phishing-nyckel.")]
	public int? PageKey { get; set; }

	[RegularExpression(Pattern.Common.AnythingMultiLine)]
    [Required(ErrorMessage = "Skriv HTML.")]
    [StringLength(
        MaxLengths.Domain.PayloadPage.Html,
        ErrorMessage = "Skriv kortare.")]
    public string Html { get; set; }
}
